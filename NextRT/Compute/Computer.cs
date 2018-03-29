using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;
using Cloo.Bindings;
namespace NextRT.Compute
{
    public class Computer
    {
        public ComputePlatform platform;
        public IList<ComputeDevice> devices;
        public Computer()
        {

            devices = new List<ComputeDevice>();
            platform = ComputePlatform.Platforms[0];
            Console.WriteLine("Device:" + platform.Name);

        }
    }
}
