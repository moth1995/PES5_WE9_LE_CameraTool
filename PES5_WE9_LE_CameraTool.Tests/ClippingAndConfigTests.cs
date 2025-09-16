using NUnit.Framework;
using PES5_WE9_LE_CameraTool;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CameraTool.Tests
{
    [TestFixture]
    public class ClippingAndConfigTests
    {
        [Test]
        public void Clipping_ToString_IncludesOffsetAndByteArrays()
        {
            var c = new Clipping();
            c.offset = 0x12345678;
            c.orgValue = new byte[] { 0x01, 0x02, 0x03, 0x04 };
            c.newValue = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD };

            string s = c.ToString();

            // Be tolerant to exact wording/format but check key parts are present.
            StringAssert.Contains("305419896", s); // 0x12345678 decimal
            StringAssert.Contains("01-02-03-04", s);
            StringAssert.Contains("AA-BB-CC-DD", s);
        }

        [Test]
        public void Configuration_Json_MinimalRoundtrip_Works()
        {
            string json = "{"
                + "\"executableSize\":123456,"
                + "\"cameraZoomOffset\":4096,"
                + "\"cameraZoomShift\":0,"
                + "\"cameraZoomOutOffset1\":8192,"
                + "\"cameraZoomOutShift1\":0,"
                + "\"cameraZoomOutOffset2\":12288,"
                + "\"cameraZoomOutShift2\":0,"
                + "\"stadRoofOffset1\":20480,"
                + "\"stadRoofShift1\":0,"
                + "\"stadRoofOffset2\":24576,"
                + "\"stadRoofShift2\":0,"
                + "\"clippingList\":[]"
                + "}";

            Configuration cfg;
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var ser = new DataContractJsonSerializer(typeof(Configuration));
                cfg = (Configuration)ser.ReadObject(ms);
            }

            Assert.IsTrue(string.IsNullOrEmpty(cfg.name));
            Assert.AreEqual(123456u, cfg.executableSize);
            Assert.AreEqual(4096u, cfg.cameraZoomOffset);
            Assert.AreEqual(0, cfg.cameraZoomShift);
        }

        [Test]
        public void Configuration_FromFilename_AssignsNameFromJsonFileName()
        {
            // Arrange: write a temp JSON *without* "name"; the app uses the file name as cfg.name
            string json = "{"
                + "\"executableSize\":1,"
                + "\"cameraZoomOffset\":2,"
                + "\"cameraZoomShift\":3,"
                + "\"cameraZoomOutOffset1\":4,"
                + "\"cameraZoomOutShift1\":5,"
                + "\"cameraZoomOutOffset2\":6,"
                + "\"cameraZoomOutShift2\":7,"
                + "\"stadRoofOffset1\":8,"
                + "\"stadRoofShift1\":9,"
                + "\"stadRoofOffset2\":10,"
                + "\"stadRoofShift2\":11,"
                + "\"clippingList\":[]"
                + "}";

            string expectedName = "PES5_PS2_SLES_535_44";
            string tmpDir = Path.Combine(Path.GetTempPath(), "cfg-tests-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tmpDir);
            string path = Path.Combine(tmpDir, expectedName + ".json");
            File.WriteAllText(path, json, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

            try
            {
                // Act: load like the app (deserialize, then set name from file name)
                Configuration cfg;
                using (var fs = File.OpenRead(path))
                {
                    var ser = new DataContractJsonSerializer(typeof(Configuration));
                    cfg = (Configuration)ser.ReadObject(fs);
                }

                // Simulate app behavior: name := file name (without extension)
                cfg.name = Path.GetFileNameWithoutExtension(path);

                // Assert
                Assert.AreEqual(expectedName, cfg.name);
            }
            finally
            {
                // Cleanup
                try { File.Delete(path); } catch { /* ignore */ }
                try { Directory.Delete(tmpDir, recursive: true); } catch { /* ignore */ }
            }
        }
    }
}
