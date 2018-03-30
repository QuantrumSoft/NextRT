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
        public RayTraceRenderer Ren1;
        public override void Start()
        {

            base.Start();
           // Tex1 = new TexGL(new Tex2D("dat/test1.png"));
         //   Console.WriteLine("TexW:" + Tex1.IT.W + " TexH:" + Tex1.IT.H);
            Scene1 = new SceneGraph();
            Ent1 = EntityImport.Import("dat/3d/tt4.3ds");
            Cam1 = new NodeCamera();
            Ren1 = new RayTraceRenderer();
//            Ren1.Sync();

            Scene1.Add(Ent1);

            Ren1.Scene = Scene1;
            Ren1.AddCam(Cam1);
            Cam1.Position = new OpenTK.Vector3(0, 5, 500);

            Cam1.LookAt(0, 0, 0);
            //Cam1.Position = new OpenTK.Vector3(0, 5, -240);
            // Cam1.SetRotate(0, 0, 0);
            //    Ent1.Child.Add(Ent1.Child[0]);
            Ent1.SetRotate(0,0, 90);
           
            var Mat1 = new NextRT.Material.Material();
            Mat1.Diffuse = new TexGL(new Tex2D("dat/test1.png"));

            foreach(var msh in Ent1.AllMeshes)
            {
                msh.Mat = Mat1;
            }
            

        }
        public override void Stop()
        {
  //          base.Stop();
        }
        public override void Render()
        {
//            base.Render();

            Ren1.Render();


        }
        float x = 0, y = 0;
        public override void Update()
        {
           // y = y + 2;
            //Ent1.SetRotate(y, y, 0);
            if (NextRT.Inputs.Input.KeyIn(OpenTK.Input.Key.Enter))
            {
                Cam1.LookAt(0, 0, 0);
            }
            if (NextRT.Inputs.Input.KeyIn(OpenTK.Input.Key.Space))
            {
                y = y + NextRT.Inputs.Input.MXD;
                x = x + NextRT.Inputs.Input.MYD;
                Cam1.SetRotate(x, y, 0);
                NextRT.Inputs.Input.MXD = 0;
                NextRT.Inputs.Input.MYD = 0;
            }
            else
            {
                NextRT.Inputs.Input.MXD = 0;
                NextRT.Inputs.Input.MYD = 0;
            }
            if (NextRT.Inputs.Input.KeyIn(OpenTK.Input.Key.W))
            {
                Cam1.Move(0, 0, -3);
            }
            if (NextRT.Inputs.Input.KeyIn(OpenTK.Input.Key.A))
            {
                Cam1.Move(-3, 0, 0);
            }
            if(NextRT.Inputs.Input.KeyIn(OpenTK.Input.Key.D))
            {
                Cam1.Move(3, 0, 0);
            }
            if (NextRT.Inputs.Input.KeyIn(OpenTK.Input.Key.S))
            {
                Cam1.Move(0, 0, 6 );
            }
            //Cam1.SetRotate(0, y, 0);
        //    Console.WriteLine("State updating.");
           // base.Update();
        }

    }
    public class TestApp1 : NextApp
    {
        public TestApp1() : base(800,600,"NextRT TestApp 1",false)
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
