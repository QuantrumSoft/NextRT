using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Tex;
namespace NextRT.Material
{
    public class Material
    {
        public TexGL Diffuse
        {
            get
            {
                return _Diffuse;
            }
            set
            {
                _Diffuse = value;
            }
        }
        public TexGL _Diffuse = null;
    }
}
