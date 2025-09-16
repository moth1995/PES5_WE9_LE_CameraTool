using System;
using System.IO;
using NUnit.Framework;
using PES5_WE9_LE_CameraTool;

namespace CameraTool.Tests
{
    [TestFixture]
    public class IntegrationCameraOffsetsTests
    {
        /// <summary>
        /// Utility that writes a float using the exact [2][3] ...shift... [0][1] pattern
        /// your tool expects in the target executable.
        /// </summary>
        private static void WriteSegmentedFloat(Stream s, uint offset, int shift, float value)
        {
            var bw = new BinaryWriter(s);
            var bytes = BitConverter.GetBytes(value); // little-endian: [0][1][2][3]

            // Write [2][3] at offset
            s.Seek(offset, SeekOrigin.Begin);
            bw.Write(bytes[2]);
            bw.Write(bytes[3]);

            // Jump by shift from current, then write [0][1]
            s.Seek(shift, SeekOrigin.Current);
            bw.Write(bytes[0]);
            bw.Write(bytes[1]);
        }

        [Test]
        public void CameraZoom_ReadThenWrite_Roundtrip_PositiveShift()
        {
            // Example offsets similar to what your JSON config provides
            uint cameraZoomOffset = 0x100;
            int cameraZoomShift = 12;

            float initialZoom = 1.25f;
            float newZoom = 1.50f;

            byte[] exe = new byte[4096];
            using (var ms = new MemoryStream(exe, writable: true))
            using (var br = new BinaryReader(ms))
            using (var bw = new BinaryWriter(ms))
            {
                // Simulate original bytes in the EXE
                WriteSegmentedFloat(ms, cameraZoomOffset, cameraZoomShift, initialZoom);

                // Read via tool logic
                ms.Position = 0;
                float read1 = BinaryPatcher.ReadFloatWithShift(ms, br, cameraZoomOffset, cameraZoomShift);
                Assert.AreEqual(initialZoom, read1);

                // Write new bytes via tool logic
                var newBytes = BitConverter.GetBytes(newZoom);
                BinaryPatcher.WriteFloatWithShift(ms, bw, cameraZoomOffset, newBytes, cameraZoomShift);

                // Read again and confirm change
                ms.Position = 0;
                float read2 = BinaryPatcher.ReadFloatWithShift(ms, br, cameraZoomOffset, cameraZoomShift);
                Assert.AreEqual(newZoom, read2);
            }
        }

        [Test]
        public void CameraZoom_ReadThenWrite_Roundtrip_NegativeShift()
        {
            uint offset = 0x200;
            int shift = -6;

            float v1 = 0.875f;
            float v2 = 0.5f;

            byte[] exe = new byte[4096];
            using (var ms = new MemoryStream(exe, writable: true))
            using (var br = new BinaryReader(ms))
            using (var bw = new BinaryWriter(ms))
            {
                // Arrange original value
                WriteSegmentedFloat(ms, offset, shift, v1);

                // Read original
                ms.Position = 0;
                float got1 = BinaryPatcher.ReadFloatWithShift(ms, br, offset, shift);
                Assert.AreEqual(v1, got1);

                // Write updated value
                BinaryPatcher.WriteFloatWithShift(ms, bw, offset, BitConverter.GetBytes(v2), shift);

                // Read updated
                ms.Position = 0;
                float got2 = BinaryPatcher.ReadFloatWithShift(ms, br, offset, shift);
                Assert.AreEqual(v2, got2);
            }
        }

        [Test]
        public void MultipleCameraFields_CanBeUpdatedIndependently()
        {
            // Simulate two camera-related fields with distinct offsets/shifts.
            uint zoomOffset = 0x300; int zoomShift = 10;
            uint roofOffset = 0x380; int roofShift = 6;

            float zoom0 = 1.0f, zoom1 = 1.3f;
            float roof0 = 0.0f, roof1 = 0.25f;

            byte[] exe = new byte[8192];
            using (var ms = new MemoryStream(exe, writable: true))
            using (var br = new BinaryReader(ms))
            using (var bw = new BinaryWriter(ms))
            {
                // Seed both fields
                WriteSegmentedFloat(ms, zoomOffset, zoomShift, zoom0);
                WriteSegmentedFloat(ms, roofOffset, roofShift, roof0);

                // Read both
                ms.Position = 0;
                Assert.AreEqual(zoom0, BinaryPatcher.ReadFloatWithShift(ms, br, zoomOffset, zoomShift));
                ms.Position = 0;
                Assert.AreEqual(roof0, BinaryPatcher.ReadFloatWithShift(ms, br, roofOffset, roofShift));

                // Update zoom only
                ms.Position = 0;
                BinaryPatcher.WriteFloatWithShift(ms, bw, zoomOffset, BitConverter.GetBytes(zoom1), zoomShift);

                // Verify zoom changed and roof did not
                ms.Position = 0;
                Assert.AreEqual(zoom1, BinaryPatcher.ReadFloatWithShift(ms, br, zoomOffset, zoomShift));
                ms.Position = 0;
                Assert.AreEqual(roof0, BinaryPatcher.ReadFloatWithShift(ms, br, roofOffset, roofShift));

                // Update roof
                ms.Position = 0;
                BinaryPatcher.WriteFloatWithShift(ms, bw, roofOffset, BitConverter.GetBytes(roof1), roofShift);

                // Verify both are now updated
                ms.Position = 0;
                Assert.AreEqual(zoom1, BinaryPatcher.ReadFloatWithShift(ms, br, zoomOffset, zoomShift));
                ms.Position = 0;
                Assert.AreEqual(roof1, BinaryPatcher.ReadFloatWithShift(ms, br, roofOffset, roofShift));
            }
        }
    }
}
