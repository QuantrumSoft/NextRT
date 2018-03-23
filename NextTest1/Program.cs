using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT;
using NextRT.Core;
using NextRT.Draw;
using NextRT.Tex;
namespace NextTest1
{
    public class State1 : AppState
    {
        public TexGL Tex1;
        public override void Start()
        {
            base.Start();
            Tex1 = new TexGL(new Tex2D("dat/test1.png"));
            Console.WriteLine("TexW:" + Tex1.IT.W + " TexH:" + Tex1.IT.H);
        }
        public override void Stop()
        {
            base.Stop();
        }
        public override void Render()
        {
            base.Render();

            Pen.Rect(20, 20, 200, 200, new NextRT.Material.Color(1, 1, 1, 1));
            Pen.Image(20, 200, 200, 200, new NextRT.Material.Color(1, 1, 1, 1), Tex1);
        }
        public override void Update()
        {
        //    Console.WriteLine("State updating.");
            base.Update();
        }

    }
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
            NextApp.StartState = new State1();
            var app = new TestApp1();
            
            app.Run();

        }
    }
}
