using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;
namespace NextRT.Compute
{
    public class ComBuffer <TYPE> where TYPE : struct
    {
        public ComputeBuffer<TYPE> Buf
        {
            get;
            set;
        }
        public bool ReadOnly
        {
            get;
            set;
        }
        public ComBuffer(bool read,bool copy,int count,TYPE[] data)
        {
            var cf = read ? ComputeMemoryFlags.ReadOnly : ComputeMemoryFlags.WriteOnly;
            if (copy)
            {
                var c2 = ComputeMemoryFlags.CopyHostPointer;
                cf = cf | c2;
            }
            Buf = new ComputeBuffer<TYPE>(Computer.scontext, cf,  data);
        }
        public ComBuffer(bool read,bool copy,int count)
        {
            var cf = read ? ComputeMemoryFlags.ReadOnly : ComputeMemoryFlags.WriteOnly;
            var c2 = ComputeMemoryFlags.AllocateHostPointer;
            cf = cf | c2;
            Buf = new ComputeBuffer<TYPE>(Computer.scontext, cf, count);
            
        }
    }
}
