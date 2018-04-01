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
        ComBuffer<float> RenLights;
        float[] RenTex;
        float[] RenLightData;
        int MeshCount;
        int RayCount = 0;        
        bool initdone = false;
        float[] RayData;
        List<float> MeshData;
        void ProcessScene()
        {
            RenLightData = new float[Scene.Lights.Count * 7];

            ProcessNode(Scene.Root);
        }
        void UpdateLights()
        {
            int ri = 0;
            foreach(var l in Scene.Lights)
            {
                RenLightData[ri++] = l.Position.X;
                RenLightData[ri++] = l.Position.Y;
                RenLightData[ri++] = l.Position.Z;
                RenLightData[ri++] = l.Diffuse.R;
                RenLightData[ri++] = l.Diffuse.G;
                RenLightData[ri++] = l.Diffuse.B;
                RenLightData[ri++] = l.Range;
            }
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
                            

                            MeshData.Add(v.X);
                            MeshData.Add(v.Y);
                            MeshData.Add(v.Z);

                            v = new OpenTK.Vector3(tri.Vertices[i].NX, tri.Vertices[i].NY, tri.Vertices[i].NZ);

                            nv = ent.Transform(v.X, v.Y, v.Z);

                            MeshData.Add(nv.X);
                            MeshData.Add(nv.Y);
                            MeshData.Add(nv.Z);
                        }
                      //  MeshData.Add(1.0f);//Trans
                     //   MeshData.Add(0.4f);//Reflect
                    }
                }
            }
            foreach(var nc in n.Child)
            {
                ProcessNode(nc);
            }
        }
        public int ThreadCount = 2;

        public Job.JobControl JC = new Job.JobControl();
        public void SetupRays()
        {
            int rays = Core.Globals.WinWidth * Core.Globals.WinHeight;

            int rPert = rays / ThreadCount;

            var cam = Cams[0];

            float bw = 200.0f;
            float bh = 150.0f;

            float mx, my, mz;

            mx = cam.Position.X;
            my = cam.Position.Y;
            mz = cam.Position.Z;
            float rx, ry, rz;
            int ray = 0;
            //float xf, yf;

            float ax, ay;
            int ri = 0;
            RaysJob.RayData = RayData;
            RaysJob.Cam = Cams[0];
            
            for (int i = 0; i < ThreadCount; i++)
            {
                RayHold rh = new RayHold();

                rh.RayStart = ri;
                rh.RayNum = rPert;

                rh.aX = new float[rPert];
                rh.aY = new float[rPert];

                for (float rn = 0; rn < rPert; rn++)
                {
                    ry = (int)(ri + rn) / (int)Core.Globals.WinWidth;
                    rx = (int)(ri+rn) - (int)((int)ry * Core.Globals.WinWidth);
                    //C//onsole.WriteLine("x:"+rx+" ry:"+ry)
                       // ;
                    float xf = rx / (float)Core.Globals.WinWidth;
                    float yf = ry / (float)Core.Globals.WinHeight;
                    xf = -1 + xf * 2.0f;
                    yf = -1 + yf * 2.0f;
                    ax = (bw * xf);
                    ay = (bh * yf);
                   // Console.WriteLine("ax:" + ax + " ay:" + ay);
                    rh.aX[(int)rn] = ax;
                    rh.aY[(int)rn] = ay;
                }


                ri += rPert;
                var rj = new RaysJob();
                rj.Obj = rh as object;
                JC.Add(rj);
               

            }

            JC.Start();
        }
        public void GenRays()
        {
            var cam = Cams[0];

            float bw = 200.0f;
            float bh = 100.0f;

            float mx, my, mz;

            mx = cam.Position.X;
            my = cam.Position.Y;
            mz = cam.Position.Z;

            int ray = 0;
            for(float y = 0; y < Core.Globals.WinHeight; y++)
            {
                for(float x = 0; x < Core.Globals.WinWidth; x++)
                {
                    float xf = x / (float)Core.Globals.WinWidth;
                    float yf = y / (float)Core.Globals.WinHeight;
                    xf = -1 + xf * 2.0f;
                    yf = -1 + yf * 2.0f;
                    float ax = (bw * xf);
                    float ay = (bh * yf);
                  //  Console.WriteLine("XF:" + xf + " YF:" + yf);
                    OpenTK.Vector3 rayS = cam.Transform(ax, ay, 0);
                //  Console.WriteLine("ax:" + ax + " aY:" + ay);
                    RayData[ray] = cam.Position.X+rayS.X;
                    RayData[ray + 1] = cam.Position.Y+rayS.Y;
                    RayData[ray + 2] = cam.Position.Z+rayS.Z;
                   // Console.WriteLine("RX:" + RayData[ray] + " RY:" + RayData[ray + 1] + " RZ:" + RayData[ray + 2]);
                    float fx, fy, fz;
                    OpenTK.Vector3 fv = -cam.Transform(0, 0, 500);

                    RayData[ray + 3] = fv.X;
                       RayData[ray + 4] = fv.Y;
                    RayData[ray + 5] = fv.Z;


                    ray += 6;


                }
            }
            RenRays = new ComBuffer<float>(true, true, RayCount * 6,ref RayData);
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
            //   GenRays();
            SetupRays();
            float[] dat = MeshData.ToArray();

            RenMesh = new ComBuffer<float>(true, true, MeshData.Count(),ref dat);
            RenRays = new ComBuffer<float>(true, true, RayCount * 6,ref RayData);
  
            RenOut = new ComBuffer<float>(false, false, (Core.Globals.WinWidth * Core.Globals.WinHeight)*3);

            UpdateLights();

            RenLights = new ComBuffer<float>(true, true, Scene.Lights.Count * 7, ref RenLightData);

            RenKern.SetFloat(0, RenRays);
            RenKern.Kern.SetValueArgument<int>(1, RayCount);
            RenKern.SetFloat(2, RenMesh);
            RenKern.Kern.SetValueArgument<int>(3, 2000);
            RenKern.SetFloat(4, RenOut);
            RenKern.SetFloat(5, RenLights);
            RenKern.Kern.SetValueArgument<int>(6, Scene.Lights.Count);
        }
        long rc = 0;
        public override void Render()
        {

            JC.Update();
            var ren = false;
            if (JC.Rounds > rc)
            {
                rc = JC.Rounds;
                ren = true;
            }
            //Console.WriteLine("Rounds:" + JC.Rounds);
            if (!initdone)
            {
                initdone = true;
                InitCL();
            }
            //    GenRays();
            //RenKern.Kern.

            // RenRays.Buf.Dispose();
            if (ren)
            {
                RenEvents.WriteFloat(RenRays, ref RaysJob.RayData);
                //         RenRays = new ComBuffer<float>(true, true, RayCount * 6, ref RayData);
                RenKern.SetFloat(0, RenRays);

                RenEvents.Run(RenKern, RayCount);
                RenEvents.Wait();
                RenEvents.ReadFloat(RenOut, ref RenTex);
            }





            if (ren)
            {
                if (Frame == null)
                {
                    Frame = new Tex.TexGL(Core.Globals.WinWidth, Core.Globals.WinHeight, ref RenTex);
                }
                else
                {
                    Frame.Upload(ref RenTex);
                }
            }
            Draw.Pen.Begin2D();
            Draw.Pen.Image(0, 0, Core.Globals.WinWidth,Core.Globals.WinHeight, new Material.Color(1, 1, 1, 1), Frame);
           // Console.WriteLine("Ren:");
        }
        public Tex.TexGL Frame = null;
    }
    public class RayHold
    {
        public int RayStart = 0;
        public int RayNum = 0;
        public NodeCamera Cam;
        public float bw, bh, rx, ry, rz, cx, cy, cz;
        public float[] aX, aY;
    }
}
