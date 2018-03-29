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

        public virtual Matrix4 WorldMatrix
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
        
        public virtual Matrix4 Rotation
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
        public Matrix4 _Rotation = Matrix4.RotateX(0);

        public virtual Vector3 Position
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

        public virtual Vector3 Scele
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

        public void LookAt(Vector3 pos)
        {
            _Rotation = Matrix4.LookAt(Vector3.Zero, pos - _Position, Vector3.UnitY);
        }

        public void LookAt(float x,float y,float z)
        {
            LookAt(new Vector3(x, y, z));
        }

        public void SetRotate(float pitch,float yaw,float roll=0)
        {
            var p = Matrix4.CreateRotationX(OpenTK.MathHelper.DegreesToRadians(pitch));
            var y = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
            var r = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(roll));

            var fm = y * p;

            _Rotation = fm;

        }

        public void SetPosition(float x,float y,float z)
        {

            _Position = new Vector3(x, y, z);

        }

        public void SetScale(float x=1,float y=1,float z = 1)
        {

            _Scale = new Vector3(x, y, z);

        }

    }
}
