using System.Collections.Generic;
using System.Runtime.Serialization;
namespace PES5_WE9_LE_CameraTool
{
    [DataContract]
    internal class Configuration
    {
        [DataMember]
        public uint executableSize;
        [DataMember]
        public uint cameraZoomOffset;
        [DataMember]
        public int cameraZoomShift;
        [DataMember]
        public uint cameraZoomOutOffset1;
        [DataMember]
        public int cameraZoomOutShift1;
        [DataMember]
        public uint cameraZoomOutOffset2;
        [DataMember]
        public int cameraZoomOutShift2;
        [DataMember]
        public uint stadRoofOffset1;
        [DataMember]
        public int stadRoofShift1;
        [DataMember]
        public uint stadRoofOffset2;
        [DataMember]
        public int stadRoofShift2;
        [DataMember]
        public List<Clipping> clippingList;
        public string name;
        public Configuration()
        {
            // Default values are PES5 PS2 SLES 535.44
            name = "PES5 PS2 SLES 535.44";
            executableSize = 2832208;
            cameraZoomOffset = 1036848;
            cameraZoomShift = 2;
            cameraZoomOutOffset1 = 1065800;
            cameraZoomOutShift1 = 2;
            cameraZoomOutOffset2 = 1521336;
            cameraZoomOutShift2 = 2;
            stadRoofOffset1 = 1123344;
            stadRoofOffset2 = 1120424;
            stadRoofShift1 = 2;
            stadRoofShift2 = 2;
            clippingList = new List<Clipping>
            {
                new Clipping
                {
                    offset = 1118408,
                    orgValue = new byte[] { 0x10, 0x00, 0x00, 0x45 },
                    newValue = new byte[] { 0x43, 0x42, 0x08, 0x08 }
                },
                new Clipping
                {
                    offset = 1118720,
                    orgValue = new byte[] { 0x10, 0x00, 0x00, 0x45 },
                    newValue = new byte[] { 0x43, 0x42, 0x08, 0x08 }
                },
                new Clipping
                {
                    offset = 1118544,
                    orgValue = new byte[] { 0x02, 0x00, 0x00, 0x45 },
                    newValue = new byte[] { 0x57, 0x42, 0x08, 0x08 }
                },
                new Clipping
                {
                    offset = 1120288,
                    orgValue = new byte[] { 0x02, 0x00, 0x01, 0x45 },
                    newValue = new byte[] { 0x0b, 0x44, 0x08, 0x08 }
                },
                new Clipping
                {
                    offset = 1120336,
                    orgValue = new byte[] { 0x0c, 0x00, 0x31, 0x36 },
                    newValue = new byte[] { 0x00, 0x00, 0x00, 0x00 }
                },
                new Clipping
                {
                    offset = 1118504,
                    orgValue = new byte[] { 0x02, 0x00, 0x01, 0x45 },
                    newValue = new byte[] { 0x4d, 0x42, 0x08, 0x08 }
                },
                new Clipping
                {
                    offset = 1119648,
                    orgValue = new byte[] { 0x02, 0x00, 0x01, 0x45 },
                    newValue = new byte[] { 0x6b, 0x43, 0x08, 0x08 }
                },
            };
        }
        public override string ToString()
        {
            string clippingListString = string.Join("\n", clippingList.ConvertAll(clipping => clipping.ToString()).ToArray());

            return $"Configuration:\n" +
                   $"Name: {name}\n" +
                   $"Executable Size: {executableSize}\n" +
                   $"Camera Zoom Offset: {cameraZoomOffset}\n" +
                   $"Camera Zoom Shift: {cameraZoomShift}\n" +
                   $"Camera Zoom Out Offset 1: {cameraZoomOutOffset1}\n" +
                   $"Camera Zoom Out Shift 1: {cameraZoomOutShift1}\n" +
                   $"Camera Zoom Out Offset 2: {cameraZoomOutOffset2}\n" +
                   $"Camera Zoom Out Shift 2: {cameraZoomOutShift2}\n" +
                   $"Stadium Roof Offset 1: {stadRoofOffset1}\n" +
                   $"Stadium Roof Shift 1: {stadRoofShift1}\n" +
                   $"Stadium Roof Offset 2: {stadRoofOffset2}\n" +
                   $"Stadium Roof Shift 2: {stadRoofShift2}\n" +
                   $"Clipping List:\n{clippingListString}";
        }
    }
}
