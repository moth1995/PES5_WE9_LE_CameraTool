using System;
using System.IO;
using NUnit.Framework;
using PES5_WE9_LE_CameraTool;

namespace CameraTool.Tests
{
    [TestFixture]
    public class BinaryPatcherTests
    {
        [Test]
        public void ReadFloatWithShift_PositiveShift_ReconstructsFloat()
        {
            // Arrange
            float expected = 1234.5f;
            byte[] fbytes = BitConverter.GetBytes(expected); // little-endian: [0][1][2][3]
            uint offset = 100;
            int shift = 12;

            byte[] buf = new byte[1024];
            using (var ms = new MemoryStream(buf))
            using (var bw = new BinaryWriter(ms))
            using (var br = new BinaryReader(ms))
            {
                // Write [2][3] at offset
                ms.Seek(offset, SeekOrigin.Begin);
                bw.Write(fbytes[2]);
                bw.Write(fbytes[3]);

                // Write [0][1] at offset + 2 + shift
                long pos2 = (long)offset + 2 + shift;
                ms.Seek(pos2, SeekOrigin.Begin);
                bw.Write(fbytes[0]);
                bw.Write(fbytes[1]);

                // Act
                ms.Position = 0;
                float got = BinaryPatcher.ReadFloatWithShift(ms, br, offset, shift);

                // Assert
                Assert.AreEqual(expected, got);
            }
        }

        [Test]
        public void WriteFloatWithShift_PositiveShift_WritesTwoSegments()
        {
            // Arrange
            float value = -42.75f;
            byte[] fbytes = BitConverter.GetBytes(value); // [0][1][2][3]
            uint offset = 256;
            int shift = 8;

            byte[] buf = new byte[1024];
            using (var ms = new MemoryStream(buf))
            using (var bw = new BinaryWriter(ms))
            using (var br = new BinaryReader(ms))
            {
                // Act
                BinaryPatcher.WriteFloatWithShift(ms, bw, offset, fbytes, shift);

                // Assert: [2][3] at offset
                ms.Seek(offset, SeekOrigin.Begin);
                Assert.AreEqual(fbytes[2], br.ReadByte());
                Assert.AreEqual(fbytes[3], br.ReadByte());

                // Assert: [0][1] at offset + 2 + shift
                long pos2 = (long)offset + 2 + shift;
                ms.Seek(pos2, SeekOrigin.Begin);
                Assert.AreEqual(fbytes[0], br.ReadByte());
                Assert.AreEqual(fbytes[1], br.ReadByte());
            }
        }

        [Test]
        public void ReadWrite_WithNegativeShift_AreSymmetric()
        {
            // Arrange
            float expected = 0.03125f;
            byte[] fbytes = BitConverter.GetBytes(expected);
            uint offset = 400;
            int shift = -10;

            byte[] buf = new byte[2048];
            using (var ms = new MemoryStream(buf))
            using (var bw = new BinaryWriter(ms))
            using (var br = new BinaryReader(ms))
            {
                // Write using the method
                BinaryPatcher.WriteFloatWithShift(ms, bw, offset, fbytes, shift);

                // Read back using the method
                ms.Position = 0;
                float got = BinaryPatcher.ReadFloatWithShift(ms, br, offset, shift);

                // Assert
                Assert.AreEqual(expected, got);
            }
        }

        [Test]
        public void WriteFloatWithShift_OffsetZero_DoesNothing()
        {
            // Arrange
            float value = 99.5f;
            byte[] fbytes = BitConverter.GetBytes(value);
            uint offset = 0; // early-return by design
            int shift = 16;

            byte[] buf = new byte[64];
            for (int i = 0; i < buf.Length; i++) buf[i] = 0xCC; // sentinel pattern

            using (var ms = new MemoryStream(buf, writable: true))
            using (var bw = new BinaryWriter(ms))
            {
                // Act
                BinaryPatcher.WriteFloatWithShift(ms, bw, offset, fbytes, shift);
            }

            // Assert: nothing changed
            foreach (var b in buf)
                Assert.AreEqual(0xCC, b);
        }

        [Test]
        public void ReadFloatWithShift_InsufficientData_ThrowsEndOfStream()
        {
            // Arrange
            byte[] buf = new byte[110];
            using (var ms = new MemoryStream(buf))
            using (var br = new BinaryReader(ms))
            {
                uint offset = 109; // only 1 byte left from offset
                int shift = 0;

                // Act + Assert
                Assert.Throws<EndOfStreamException>(() =>
                {
                    BinaryPatcher.ReadFloatWithShift(ms, br, offset, shift);
                });
            }
        }

        [Test]
        public void ReadFloatWithShift_SeekBeforeBeginning_ThrowsIOException()
        {
            // This validates that a too-large negative shift causes an IOException,
            // which is the runtime behavior of Stream.Seek when position < 0.
            byte[] buf = new byte[32];
            using (var ms = new MemoryStream(buf))
            using (var br = new BinaryReader(ms))
            {
                uint offset = 4;
                int shift = -10; // 4 + 2 + (-10) => tries to move before beginning

                Assert.Throws<IOException>(() =>
                {
                    BinaryPatcher.ReadFloatWithShift(ms, br, offset, shift);
                });
            }
        }
    }
}
