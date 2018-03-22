using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
namespace NextRT
{
    public static class Globals
    {
        public static int WinWidth, WinHeight;
        public static string AppName = "";
        public static bool FullScreen = false;
    }
    public class NextApp : GameWindow
    {

        public NextApp(int width,int height,string app,bool full) : base(width,height,OpenTK.Graphics.GraphicsMode.Default,app,full ? GameWindowFlags.Fullscreen : GameWindowFlags.FixedWindow)
        {
            Globals.WinWidth = width;
            Globals.WinHeight = height;
            Globals.AppName = app;
            Globals.FullScreen = full;
            SetupGL(width, height);

        }

        private static void SetupGL(int width, int height)
        {
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.AlphaTest);
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.DepthClamp);
            GL.Disable(EnableCap.ScissorTest);

            GL.Viewport(0, 0, width, height);
            GL.ClearColor(0.5f, 0.5f, 0.5f, 0.0f);
        }

        protected override void OnLoad(EventArgs e)
        {
            SetupGL(Globals.WinWidth, Globals.WinHeight);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Console.WriteLine("Updating app.");
        }

    }
}
