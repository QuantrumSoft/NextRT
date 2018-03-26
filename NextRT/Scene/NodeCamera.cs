using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace NextRT.Scene
{
    public class NodeCamera : Node3D
    {

        public override Matrix4 WorldMatrix
        {
            get
            {
                return base.WorldMatrix.Inverted();
            }
        }

        public Vector4 Viewport
        {
            get
            {
                return _Viewport;
            }
            set
            {
                _Viewport = value;
            }
        }
        public Vector4 _Viewport = new Vector4(0, 0, Core.Globals.WinWidth, Core.Globals.WinHeight);

        public float RenderMinZ
        {
            get
            {
                return _RenderMinZ;
            }
            set
            {
                _RenderMinZ = value;
            }
        }
        public float _RenderMinZ = 0.1f;


        public float RenderMaxZ
        {
            get
            {
                return _RenderMaxZ;
            }
            set
            {
                _RenderMaxZ = value;
            }
        }
        public float _RenderMaxZ = 1000.0f;

        public float FOV
        {
            get
            {
                return _FOV;
            }
            set
            {
                _FOV = value;
            }
        }
        public float _FOV = 45.0f;

        
    }




}
