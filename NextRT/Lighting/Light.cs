using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using NextRT.Scene;
using NextRT.Material;
namespace NextRT.Lighting
{
    public enum LightType
    {
        Point,Dir,Spot,Amb
    }
    public class Light : Node3D
    {
        public LightType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
        public LightType _Type = LightType.Point;
        
        public Color Diffuse
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
        public Color _Diffuse = new Color(1, 1, 1, 1);

        public Color Specular
        {
            get
            {
                return _Specular;
            }
            set
            {
                _Specular = value;
            }
        }
        public Color _Specular = new Color(1, 1, 1, 1);
        
        public float Range
        {
            get
            {
                return _Range;
            }
            set
            {
                _Range = value;
            }
        }
        public float _Range = 200.0f;

    }
}

