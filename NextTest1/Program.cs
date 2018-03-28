using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT;
using NextRT.Core;
using NextRT.Draw;
using NextRT.Tex;
using NextRT.Scene;
using NextRT.Import;
using NextRT.Render;
namespace NextTest1
{
    public class State1 : AppState
    {
        public TexGL Tex1;
        public SceneGraph Scene1;
        public NodeEntity Ent1;
        public NodeCamera Cam1;
        public SimpleRenderer Ren1;
        public override void Start()
        {

            base.Start();
            Tex1 = new TexGL(new Tex2D("dat/test1.png"));
            Console.WriteLine("TexW:" + Tex1.IT.W + " TexH:" + Tex1.IT.H);
            Scene1 = new SceneGraph();
            Ent1 = EntityImport.Import("dat/3d/TestScene1.3ds");
            Cam1 = new NodeCamera();
            Ren1 = new SimpleRenderer();

            Scene1.Add(Ent1);

            Ren1.Scene = Scene1;
            Ren1.AddCam(Cam1);

        }
        public override void Stop()
        {
            base.Stop();
        }
        public override void Render()
        {
            base.Render();

            Ren1.Render();

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
