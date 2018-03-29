using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Compute;
using NextRT.Scene;
namespace NextRT.Render
{
    public class RayTraceRenderer : SceneRenderer
    {


        public override void Sync()
        {
            base.Sync();
        }

        ComProg RenProg;
        ComKern RenKern;
        ComEvents RenEvents;
        ComBuffer<float> RenRays;
        ComBuffer<float> RenOut;
        ComBuffer<float> RenMesh;
        float[] RenTex;
        int MeshCount;
        int RayCount = 0;        
        bool initdone = false;
        float[] RayData;
        List<float> MeshData;
        void ProcessScene()
        {
            ProcessNode(Scene.Root);
        }
        void ProcessNode(SceneNode n)
        {
            if(n is NodeEntity)
            {
                var ent = n as NodeEntity;
                foreach(var mesh in ent.Meshes)
                {
                    foreach(var tri in mesh.Tris)
                    {
                        
                        for(int i = 0; i < 3; i++)
                        {

                            OpenTK.Vector3 v = new OpenTK.Vector3(tri.Vertices[i].X, tri.Vertices[i].Y, tri.Vertices[i].Z);

                            OpenTK.Vector3 nv = ent.Transform(v.X, v.Y, v.Z);
                            

                            MeshData.Add(nv.X);
                            MeshData.Add(nv.Y);
                            MeshData.Add(nv.Z);

                            v = new OpenTK.Vector3(tri.Vertices[i].NX, tri.Vertices[i].NY, tri.Vertices[i].NZ);

                            nv = ent.Transform(v.X, v.Y, v.Z);

                            MeshData.Add(nv.X);
                            MeshData.Add(nv.Y);
                            MeshData.Add(nv.Z);
                        }
                        MeshData.Add(1.0f);//Trans
                        MeshData.Add(0.4f);//Reflect
                    }
                }
            }
            foreach(var nc in n.Child)
            {
                ProcessNode(nc);
            }
        }
        public void GenRays()
        {
            var cam = Cams[0];

            float bw = 2.0f;
            float bh = 1.0f;

            float mx, my, mz;

            mx = cam.Position.X;
            my = cam.Position.Y;
            mz = cam.Position.Z;

            int ray = 0;
            for(float y = 0; y < Core.Globals.WinHeight; y++)
            {
                for(float x = 0; x < Core.Globals.WinWidth; x++)
                {
                    float xf = x / Core.Globals.WinWidth;
                    float yf = y / Core.Globals.WinHeight;
                    xf = -1 + xf * 2.0f;
                    yf = -1 * yf * 2.0f;
                    float ax = (-bw + (bw * 2)) * xf;
                    float ay = (-bh + (bh * 2)) * yf;

                    OpenTK.Vector3 rayS = cam.Transform(ax, ay, 0);

                    RayData[ray] = rayS.X;
                    RayData[ray + 1] = rayS.Y;
                    RayData[ray + 2] = rayS.Z;

                    float fx, fy, fz;
                    OpenTK.Vector3 fv = cam.Transform(0, 0, 1000);

                    RayData[ray + 3] = fv.X;
                    RayData[ray + 4] = fv.Y;
                    RayData[ray + 5] = fv.Z;


                    ray += 6;


                }
            }
            RenRays = new ComBuffer<float>(true, true, RayCount * 6, RayData);
        }   
        public void InitCL()
        {
            RenProg = new ComProg("dat/cl/render.cl");
            RenKern = new ComKern(RenProg, "RenderMesh");
            RenEvents = new ComEvents();

            
            RayCount = Core.Globals.WinWidth * Core.Globals.WinHeight;
            RayData = new float[RayCount * 6];

            RenTex = new float[Core.Globals.WinWidth * Core.Globals.WinHeight * 3];

            MeshData = new List<float>();
            MeshData.Clear();
            ProcessScene();
            GenRays();
           
            RenMesh = new ComBuffer<float>(true, true, MeshData.Count(), MeshData.ToArray());
            
  
            RenOut = new ComBuffer<float>(false, false, (Core.Globals.WinWidth * Core.Globals.WinHeight)*3);

            RenKern.SetFloat(0, RenRays);
            RenKern.Kern.SetValueArgument<int>(1, RayCount);
            RenKern.SetFloat(2, RenMesh);
            RenKern.SetFloat(3, RenOut);
        }
        public override void Render()
        {
            if (!initdone)
            {
                initdone = true;
                InitCL();
            }
            GenRays();
            RenRays = new ComBuffer<float>(true, true, RayCount * 6, RayData);
            RenKern.SetFloat(0, RenRays);

            RenEvents.Run(RenKern, RayCount);

            RenEvents.ReadFloat(RenOut, RenTex);

            RenEvents.Wait();


            var fb = new Tex.Tex2D(Core.Globals.WinWidth, Core.Globals.WinHeight);


            byte[] d2 = new byte[fb.H * fb.W * 3];

            for (int y = 0; y < fb.H; y++)
            {
                for (int x = 0; x < fb.W; x++)
                {
                    int loc = (y * fb.W * 3) + (x * 3);
                    d2[loc] = (byte)(RenTex[loc] * 255.0f);
                    d2[loc + 1] = (byte)(RenTex[loc + 1] * 255.0f);
                    d2[loc + 2] = (byte)(RenTex[loc + 2] * 255.0f);
                }
            }

            fb.Data.Data = d2;

            Tex.TexGL nt = new Tex.TexGL(fb);

            Draw.Pen.Begin2D();
            Draw.Pen.Image(0, 0, fb.W, fb.H, new Material.Color(1, 1, 1, 1), nt);

        }

    }
}
