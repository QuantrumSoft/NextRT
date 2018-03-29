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
                            MeshData.Add(tri.Vertices[i].X);
                            MeshData.Add(tri.Vertices[i].Y);
                            MeshData.Add(tri.Vertices[i].Z);
                            MeshData.Add(tri.Vertices[i].NX);
                            MeshData.Add(tri.Vertices[i].NY);
                            MeshData.Add(tri.Vertices[i].NZ);
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



        }
        public void InitCL()
        {
            RenProg = new ComProg("dat/cl/render.cl");
            RenKern = new ComKern(RenProg, "RenderMesh");
            RenEvents = new ComEvents();

            RayCount = Core.Globals.WinWidth * Core.Globals.WinHeight;
            RayData = new float[RayCount * 6];

            MeshData = new List<float>();
            MeshData.Clear();
            ProcessScene();
            GenRays();

            RenMesh = new ComBuffer<float>(true, true, MeshData.Count(), MeshData.ToArray());
            
            RenRays = new ComBuffer<float>(true, true, RayCount * 6, RayData);
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
        }

    }
}
