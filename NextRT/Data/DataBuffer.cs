using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Data
{
    public class DataBuffer
    {
        public int Size = 0;
        public byte[] Data = null;
        public DataBuffer(int bytes)
        {
            Size = bytes;
            Data = new byte[Size];
        }
        public void SetData(byte[] data)
        {
            Data = data;
        }
        public void CopyData(byte[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                if (i >= Size) break;
                Data[i] = data[i];
            }
        }
    }
}
