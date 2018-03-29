using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Compute;
namespace NextRT.PostProcessing
{
    public class PostProcess
    {
        public ComProg Prog
        {
            get;
            set;
        }
        public ComKern Kern
        {
            get;
            set;
        }
        public PostProcess()
        {
            Init();
        }
        public virtual void Init()
        {

        }
        public virtual void Render()
        {

        }
        public virtual void Release()
        {

        }
    }
}
