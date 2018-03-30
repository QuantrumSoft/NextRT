using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;
namespace NextRT.Compute
{
    public class ComKern
    {
        public ComputeKernel Kern
        {
            get;
            set;
        }
        public ComKern( ComProg prog,string name )
        {
            try
            {
                Kern = prog.Prog.CreateKernel(name);
            }
            catch
            {
                Console.WriteLine(prog.Prog.GetBuildLog(Compute.Computer.scontext.Devices[0]));
            }
                if (Kern == null)
            {
                Console.WriteLine("Kernel not created.");
                while (true)
                {

                }
            }
        }
        public void SetFloat(int index,ComBuffer<float> buf)
        {
            Kern.SetMemoryArgument(index, buf.Buf);
        }
        public void SetByte(int index,ComBuffer<byte> buf)
        {
            Kern.SetMemoryArgument(index, buf.Buf);
        }
    }
}
