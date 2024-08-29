using System;
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

        public override string ToString()
        {
            string orgValueString = BitConverter.ToString(orgValue);
            string newValueString = BitConverter.ToString(newValue);

            return $"Clipping:\n" +
                   $"Offset: {offset}\n" +
                   $"Original Value: [{orgValueString}]\n" +
                   $"New Value: [{newValueString}]";
        }
    }
}
