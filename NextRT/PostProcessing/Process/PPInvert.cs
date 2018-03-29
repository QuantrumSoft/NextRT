using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
namespace NextRT.PostProcessing.Process
{
    public class PPInvert : PostProcess
    {
        public Tex.TexGL BufTex
        {
            get;
            set;
        }
        public Compute.ComKern kern;
        public Compute.ComBuffer<float> screen;
        public Compute.ComBuffer<float> output;
        public Compute.ComEvents events;
        public override void Init()
        {
            Prog = new Compute.ComProg("dat/cl/ppinvert.cl");
            BufTex = new Tex.TexGL(Core.Globals.WinWidth, Core.Globals.WinHeight);
           // screen = new Compute.ComBuffer<float>(true, false, (Core.Globals.WinWidth * Core.Globals.WinHeight) * 3);
            kern = new Compute.ComKern(Prog, "PPInvert");
            //Kern.SetFloat(0, screen);
            events = new Compute.ComEvents();
        }
        public override void Render()
        {

            GL.ReadBuffer(ReadBufferMode.Back);


            Tex.Tex2D fb = new Tex.Tex2D(Core.Globals.WinWidth,Core.Globals.WinHeight, false);

            GL.ReadPixels<byte>(0, 0,fb.W,fb.H, PixelFormat.Rgb, PixelType.UnsignedByte, fb.Data.Data);

            float[] dat = new float[fb.W * fb.H * 3];
            for(int y = 0; y < fb.H; y++)
            {
                for(int x = 0; x < fb.W; x++)
                {
                    int loc = (x * 3) + (y * fb.W * 3);
                    dat[loc] = (float)(fb.Data.Data[loc]) / 255.0f;
                    dat[loc + 1] = (float)(fb.Data.Data[loc + 1]) / 255.0f;
                    dat[loc + 2] = (float)(fb.Data.Data[loc + 2]) / 255.0f;

                }
            }

            screen = new Compute.ComBuffer<float>(true,true, fb.W * fb.H * 3, dat);
            output = new Compute.ComBuffer<float>(false, false, fb.W * fb.H * 3);

            kern.SetFloat(0, screen);
            kern.SetFloat(1, output);

            for(int i = 0; i < 512 * 512; i++)
            {
             //   if (fb.Data.Data[i] > 0)
               // {
            //        Console.WriteLine("V:1");
           //     }
            }

            events.Run(kern, fb.W * fb.H);

            events.ReadFloat(output, dat);


            events.Wait();

         

            fb = new Tex.Tex2D(fb.W, fb.H);

            byte[] d2 = new byte[fb.H * fb.W * 3];

            for(int y = 0; y < fb.H; y++)
            {
                for(int x = 0; x < fb.W; x++)
                {
                    int loc = (y * fb.W * 3) + (x * 3);
                    d2[loc] = (byte)(dat[loc] * 255.0f);
                    d2[loc + 1] = (byte)(dat[loc + 1] * 255.0f);
                    d2[loc + 2] = (byte)(dat[loc + 2] * 255.0f);
                }
            }

            fb.Data.Data = d2;

            Tex.TexGL nt = new Tex.TexGL(fb);


            //                / BufTex.Release(0);
            //BufTex.Bind(0);
            //GL.CopyTexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb8, 0, 0,Core.Globals.WinWidth,Core.Globals.WinHeight, 0);
            //BufTex.Release(0);

            //  kern.

            Draw.Pen.Begin2D();
            Draw.Pen.Image(0,0,fb.W,fb.H, new Material.Color(1, 1, 1, 1), nt);



        }
    }
}
