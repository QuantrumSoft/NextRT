using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace NextRT.Scene
{
    public class Node3D : SceneNode
    {

        public Matrix4 WorldMatrix
        {
            get
            {
                Matrix4 bm = Matrix4.Identity;

                if(_Root != null)
                {
                    if(_Root is Node3D)
                    {
                        var nr = _Root as Node3D;
                        bm = bm * nr.WorldMatrix;
                    }
                }

                bm = bm * _Rotation;
                bm = bm * Matrix4.CreateTranslation(_Position);

                return bm;

            }
        }
        
        public Matrix4 Rotation
        {
            get
            {
                return _Rotation;
            }
            set
            {
                _Rotation = value;
            }
        }
        public Matrix4 _Rotation;

        public Vector3 Position
        {
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
            }
        }
        public Vector3 _Position = Vector3.Zero;

        public Vector3 Scele
        {
            get
            {
                return _Scale;
            }
            set
            {
                _Scale = value;
            }
        }
        public Vector3 _Scale = Vector3.One;

    }
}
