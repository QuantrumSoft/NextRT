using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT;
namespace NextTest1
{
    public class TestApp1 : NextApp
    {
        public TestApp1() : base(640,480,"NextRT TestApp 1",false)
        {

        }
    }
    public class Program
    {
        static void Main(string[] args)
        {

            var app = new TestApp1();

            app.Run();

        }
    }
}
