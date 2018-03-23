using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Material;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using NextRT.Core;
using NextRT.Tex;
namespace NextRT.Draw
{
    public static class Pen
    {
        
        public static void Begin2D()
        {


            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 ort = Matrix4.CreateOrthographicOffCenter(0, Globals.WinWidth, Globals.WinHeight, 0, -1, 1);
            GL.LoadMatrix(ref ort);

        }
        public static void Image(int x,int y,int w,int h,Color col,TexGL tex)
        {

            tex.Bind(0);

            GL.Color4(col.R, col.G, col.B, col.A);

            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(x, y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(x + w, y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(x + w, y + h);
            GL.TexCoord2(0, 1);
            GL.Vertex2(x, y + h);

            GL.End();

            tex.Release(0);
        }
        public static void Rect(int x,int y,int w,int h,Color col)
        {

            GL.Color4(col.R, col.G, col.B, col.A);

            GL.Begin(BeginMode.Quads);

            GL.Vertex2(x, y);
            GL.Vertex2(x + w, y);
            GL.Vertex2(x + w, y + h);
            GL.Vertex2(x, y + h);

            GL.End();


        }

    }
}
