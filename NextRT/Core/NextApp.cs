﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using NextRT.Data;
namespace NextRT.Core
{
    public static class Globals
    {
        public static int WinWidth, WinHeight;
        public static string AppName = "";
        public static bool FullScreen = false;
    }
    public class NextApp : GameWindow
    {
        public static AppState StartState = null;
        public static Stack<AppState> States = new Stack<AppState>();
        public static void PushState(AppState state)
        {
            States.Push(state);
            state.Start();
        }
        public static AppState PopState()
        {
            var rs = States.Pop();
            if (rs != null)
            {
                rs.Stop();
            }
            return rs;
        }
        public static void PauseState()
        {
            var s = States.Last();
            if (s == null) return;
            s.Pause();

        }
        public static void ResumeState()
        {
            var s = States.Last();
            if (s == null) return;
            s.Resume();
        }
        ~NextApp()
        {
            if (States.Last() != null)
            {
                States.Last().Stop();
            }
        }
        public NextApp(int width,int height,string app,bool full) : base(width,height,OpenTK.Graphics.GraphicsMode.Default,app,full ? GameWindowFlags.Fullscreen : GameWindowFlags.FixedWindow)
        {
            Globals.WinWidth = width;
            Globals.WinHeight = height;
            Globals.AppName = app;
            Globals.FullScreen = full;
            SetupGL(width, height);

            Import.EntityImport.RegisterDefaults();

            if (StartState != null)
            {
                PushState(StartState);
            }

        }

        private static void SetupGL(int width, int height)
        {
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.AlphaTest);
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.DepthTest);
                GL.Disable(EnableCap.DepthClamp);
                GL.Disable(EnableCap.ScissorTest);
            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.StencilTest);
            GL.DepthRange(0, 1);
            GL.Viewport(0, 0, width, height);
            GL.ClearColor(0,0,0, 0.0f);
           // Draw.Pen.Begin2D();

        }

        protected override void OnLoad(EventArgs e)
        {
            SetupGL(Globals.WinWidth, Globals.WinHeight);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            States.Last().Update();

        }
        float y = 0;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            y++;
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            States.Last().Render();

            SwapBuffers();
        }
    }
}
