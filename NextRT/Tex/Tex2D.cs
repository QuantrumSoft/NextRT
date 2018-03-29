using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Data;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
namespace NextRT.Tex
{
    public class Tex2D
    {
        public int W, H;
        public bool Alpha = false;
        public DataBuffer Data = null;
        public Tex2D(string path)
        {
            Bitmap bit = new Bitmap(path);
            Data = new DataBuffer(bit.Width * bit.Height * 3);
            for(int y = 0; y < bit.Height; y++)
            {
                for(int x = 0; x < bit.Width; x++)
                {
                    var pix = bit.GetPixel(x, y);
                    int loc = (y * bit.Width * 3) + x * 3;
                    Data.Data[loc++] = pix.R;
                    Data.Data[loc++] = pix.G;
                    Data.Data[loc++] = pix.B;

                }
            }
            W = bit.Width;
            H = bit.Height;
            Alpha = false;
        }
        public Tex2D(int w,int h,bool alpha = false)
        {
            W = w;
            H = h;
            Alpha = alpha;
            int ds = w * h;
            if (alpha)
            {
                ds *= 4;
            }
            else
            {
                ds *= 3;
            }
            Data = new DataBuffer(ds);
        }
    }
    public class TexGL
    {
        public Tex2D IT = null;
        public int GLID = 0;
        public TexGL(Tex2D origin)
        {
            IT = origin;
            GenGL();

            if (origin.Alpha ==false)
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, origin.W, origin.H, 0, PixelFormat.Rgb, PixelType.UnsignedByte, origin.Data.Data);
            }
            else
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, origin.W, origin.H, 0, PixelFormat.Rgba, PixelType.UnsignedByte, origin.Data.Data);

            }
        }
        public TexGL(int w,int h)
        {
            IT = new Tex2D(w, h, false);

            GenGL();
            GL.PixelStore(PixelStoreParameter.PackAlignment, 3);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 3);
          //  GL.PixelTransfer(PixelTransferParameter.MapColor, 1);
        }
            
        public void Bind(int unit)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            GL.ClientActiveTexture(TextureUnit.Texture0 + unit);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D,GLID);

        }
       
        public void Release(int unit)
        {
           GL.ActiveTexture(TextureUnit.Texture0 + unit);
           GL.ClientActiveTexture(TextureUnit.Texture0 + unit);
            GL.Disable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);


        }

        private void GenGL()
        {
            GLID = GL.GenTexture();
            Console.WriteLine("GLtexID=" + GLID);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, GLID);
            int tpi = (int)TextureWrapMode.Clamp;

            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, ref tpi);
            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, ref tpi);

            tpi = (int)TextureMagFilter.Nearest;
            //GL.TexParameterI(TextureTarget.Texture2D,TextureParameterName.GenerateMipmap,GenerateMipmapTarget.)


            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, ref tpi);
            tpi = (int)TextureMinFilter.Nearest;
            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, ref tpi);
        }
    }
}
