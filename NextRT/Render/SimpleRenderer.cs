using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Scene;
using OpenTK;
using OpenTK.Graphics.OpenGL;
namespace NextRT.Render
{
    public class SimpleRenderer : SceneRenderer
    {

        NodeCamera _cam = null;
        public override void Render()
        {
            foreach (var cam in Cams)
            {
                _cam = cam;
                RenderNode(Scene.Root);
            }
        }

        public void RenderNode(SceneNode node)
        {
           // Console.WriteLine("RenderNode:" + node);
            if(node is NodeEntity)
            {
                var ent = node as NodeEntity;

                var mat = ent.WorldMatrix;

                var cm = _cam.WorldMatrix;


                mat = cm * mat;

                GL.MatrixMode(MatrixMode.Projection);

                var pm = Matrix4.CreatePerspectiveOffCenter(_cam.Viewport.X, _cam.Viewport.X+_cam.Viewport.Width, _cam.Viewport.Y+_cam.Viewport.Height,_cam.Viewport.Y, _cam.RenderMinZ, _cam.RenderMaxZ);
               // GL.Viewport(_cam.Viewport.X, _cam.Viewport.Y, _cam.Viewport.Width, _cam.Viewport.Height);

                GL.LoadMatrix(ref pm);

                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref mat);


                GL.Color4(255.0f,255.0f,255.0f,255.0f);

                foreach(var m in ent.Meshes)
                {

                    GL.Begin(BeginMode.Triangles);
                    foreach (var tri in m.Tris)
                    {

                        for(int i = 0; i < 3; i++)
                        {
                            //   Console.WriteLine("X:" + tri.Vertices[i].X + " Y:" + tri.Vertices[i].Y + " Z:" + tri.Vertices[i].Z);
                            GL.Color3(1.0f, 1.0f, 1.0f);
                            GL.Vertex3(tri.Vertices[i].X, tri.Vertices[i].Y, tri.Vertices[i].Z);
                        }
                      
                    }
                    GL.End();



                }



                //   Console.WriteLine("Rendering:" + ent.Name);

            }
            foreach(var node2 in node.Child)
            {
                RenderNode(node2);
            }
        }

    }
}
