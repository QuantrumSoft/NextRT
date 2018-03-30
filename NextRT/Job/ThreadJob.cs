using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace NextRT.Job
{
    public class ThreadJob
    {
        public object Obj = null;
        public bool RoundComplete
        {
            get;
            set;
        }
        public Thread T
        {
            get;
            set;
        }
        public int SleepLength = 1;
        public ThreadJob(object data = null)
        {
            T = new Thread(new ThreadStart(ThreadUpdate));
            RoundComplete = false;
        }
        public virtual void Start()
        {
            T.Start();
        }
        public void AddThread()
        {
             
        }
        public virtual void Init()
        {

        }
        public void ThreadUpdate()
        {
            while (true)
            {
                if (RoundComplete == false)
                {
                    Update();
                }
                else
                {
                    Thread.Sleep(5);
                }
                if (SleepLength > 0)
                {
                    Thread.Sleep(SleepLength);
                }
            }
        }
        public virtual void Update()
        {
            
        }
        public virtual void Done()
        {

        }

    }
}
