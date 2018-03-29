using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;
namespace NextRT.Compute
{
    public class ComEvents
    {
        public ComputeEventList List
        {
            get;
            set;
        }
        public ComputeCommandQueue Queue
        {
            get;
            set;
        }

        public ComEvents()
        {

            List = new ComputeEventList();
            Queue = new ComputeCommandQueue(Compute.Computer.scontext, Computer.scontext.Devices[0], ComputeCommandQueueFlags.None);
            

                 
        }

        public void Run(ComKern kernal,int count)
        {
            Queue.Execute(kernal.Kern, null, new long[] { count }, null, List);
        }
        public void ReadFloat(ComBuffer<float> buf, float[] to)
        {
            Queue.ReadFromBuffer<float>(buf.Buf, ref to,true, List);
        }
        public void Wait()
        {
            List.Wait();
            Queue.Finish();
        }
    }
}
