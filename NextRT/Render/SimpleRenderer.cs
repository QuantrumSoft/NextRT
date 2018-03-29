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
                GL.LoadIdentity();
                var pm = Matrix4.CreatePerspectiveOffCenter(_cam.Viewport.X, _cam.Viewport.X+_cam.Viewport.Width, _cam.Viewport.Y+_cam.Viewport.Height,_cam.Viewport.Y, _cam.RenderMinZ, _cam.RenderMaxZ);
                // GL.Viewport(_cam.Viewport.X, _cam.Viewport.Y, _cam.Viewport.Width, _cam.Viewport.Height);
                pm = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)Core.Globals.WinWidth / (float)Core.Globals.WinHeight,0.1f, 800.0f);
                GL.MultMatrix(ref pm);

                
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.MultMatrix(ref mat);
                GL.Disable(EnableCap.Texture2D);
                

                GL.Viewport(0, 0, Core.Globals.WinWidth, Core.Globals.WinHeight);

                //GL.Color4(255.0f,255.0f,255.0f,255.0f);
                GL.Color4(1.0f, 1.0f, 1.0f, 1.0f);

                foreach(var m in ent.Meshes)
                {
                
                    if(m.Mat != null)
                    {
                        m.Mat.Diffuse.Bind(0);
                    }
                //    Console.WriteLine("Rendering Mesh:" + m.Tris.Length);
                    GL.Begin(BeginMode.Triangles);
                    foreach (var tri in m.Tris)
                    {

                        for(int i = 0; i < 3; i++)
                        {
                            //   Console.WriteLine("X:" + tri.Vertices[i].X + " Y:" + tri.Vertices[i].Y + " Z:" + tri.Vertices[i].Z);
                            GL.TexCoord2(tri.Vertices[i].U, tri.Vertices[i].V);
                            GL.Vertex3(tri.Vertices[i].X, tri.Vertices[i].Y, tri.Vertices[i].Z);
                        }
                      
                    }
                    GL.End();

                    if (m.Mat != null)
                    {
                        m.Mat.Diffuse.Release(0);
                    }

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
