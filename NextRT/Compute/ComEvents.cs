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
            Queue = new ComputeCommandQueue(Compute.Computer.scontext, Computer.scontext.Devices[0], ComputeCommandQueueFlags.OutOfOrderExecution);
            

                 
        }

        public void Run(ComKern kernal,int count)
        {

            Queue.Execute(kernal.Kern, null, new long[] { count }, null, List);
        }
        public void ReadFloat(ComBuffer<float> buf,ref float[] to)
        {
            Queue.ReadFromBuffer<float>(buf.Buf, ref to, true, null); ;
        }
        public void WriteFloat(ComBuffer<float> buf,ref float[] from)
        {

            //    Queue.WriteToBuffer(from, buf.Buf, false, List);
            Queue.WriteToBuffer<float>(from, buf.Buf, false, List);
        }
        public void Wait()
        {

            //Queue.Wait(List);
            Queue.Finish();
            List.Clear();
            
            //List.Wait();
            //List.Clear();
            //Queue.Finish();

        }
    }
}
