using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Material
{
    public class Color
    {
        public float R, G, B, A;
        public Color()
        {
            R = G = B = A = 0.0f;
        }
        public Color(float r,float g,float b,float a=1.0f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}
