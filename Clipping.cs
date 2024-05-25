using System.Runtime.Serialization;
namespace PES5_WE9_LE_CameraTool
{
    [DataContract]
    internal class Clipping
    {
        [DataMember]
        public uint offset;
        [DataMember]
        public byte[] orgValue;
        [DataMember]
        public byte[] newValue;
    }
}
