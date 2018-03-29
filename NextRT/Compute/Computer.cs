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
        public static ComputeContext scontext;
        public static ComputePlatform splatform;
        public ComputePlatform platform;
        public IList<ComputeDevice> devices;
        public ComputeContextPropertyList props;
        public ComputeContext context;

        public Computer()
        {

            devices = new List<ComputeDevice>();
            platform = ComputePlatform.Platforms[0];
            Console.WriteLine("Device:" + platform.Name);

            props = new ComputeContextPropertyList(platform);
            devices.Add(platform.Devices[0]);
            context = new ComputeContext(devices, props, null, IntPtr.Zero);

            scontext = context;
            splatform = platform;
        }
    }
}
