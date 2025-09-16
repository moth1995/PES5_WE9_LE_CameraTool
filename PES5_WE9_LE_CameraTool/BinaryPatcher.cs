using System;
using System.IO;

namespace PES5_WE9_LE_CameraTool
{
    public static class BinaryPatcher
    {
        public static float ReadFloatWithShift(Stream fs, BinaryReader reader, uint offset, int shift)
        {
            if (fs == null) throw new ArgumentNullException("fs");
            if (reader == null) throw new ArgumentNullException("reader");
            if (!fs.CanSeek) throw new InvalidOperationException("Stream must be seekable.");

            byte[] valueBytes = new byte[sizeof(float)];

            fs.Seek(offset, SeekOrigin.Begin);
            valueBytes[2] = reader.ReadByte();
            valueBytes[3] = reader.ReadByte();

            fs.Seek(shift, SeekOrigin.Current);

            valueBytes[0] = reader.ReadByte();
            valueBytes[1] = reader.ReadByte();

            return BitConverter.ToSingle(valueBytes, 0);
        }

        public static void WriteFloatWithShift(Stream fs, BinaryWriter writer, uint offset, byte[] newValueBytes, int shift)
        {
            if (!Convert.ToBoolean(offset)) return;
            if (fs == null) throw new ArgumentNullException("fs");
            if (writer == null) throw new ArgumentNullException("writer");
            if (newValueBytes == null || newValueBytes.Length != 4)
                throw new ArgumentException("newValueBytes must be exactly 4 bytes.", "newValueBytes");
            if (!fs.CanSeek) throw new InvalidOperationException("Stream must be seekable.");

            fs.Seek(offset, SeekOrigin.Begin);
            writer.Write(newValueBytes[2]);
            writer.Write(newValueBytes[3]);

            fs.Seek(shift, SeekOrigin.Current);

            writer.Write(newValueBytes[0]);
            writer.Write(newValueBytes[1]);
        }
    }
}
