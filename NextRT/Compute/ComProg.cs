using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;
using System.IO;
namespace NextRT.Compute
{
    public class ComProg
    {
        public ComputeProgram Prog
        {
            get;
            set;
        }
        public ComProg(string path)
        {

            string code = File.ReadAllText(path);
            if (code.Length <2)
            {
                Console.WriteLine("CL program empty.");
                return;
            }

            Prog = new ComputeProgram(Computer.scontext, code);
            Prog.Build(null, null, null, IntPtr.Zero);




        }
    }
}
