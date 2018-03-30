using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Job
{
    public class JobControl
    {
        public long Rounds = 0;
        public List<ThreadJob> Jobs
        {
            get;
            set;
        }
        public JobControl()
        {
            Jobs = new List<ThreadJob>();

        }
        public void Start()
        {
            foreach(var j in Jobs)
            {
                j.Start();
            }
        }
        public void Add(ThreadJob j)
        {
            Jobs.Add(j);
        }
        public void Update()
        {
            int dc = 0;
            foreach(var j in Jobs)
            {
                if(j.RoundComplete ==true)
                {
                    dc++;
                }
            }
            if (dc == Jobs.Count)
            {
                foreach(var j in Jobs)
                {
                    j.RoundComplete = false;
                }
                Rounds++;
            }
        }
    }
}
