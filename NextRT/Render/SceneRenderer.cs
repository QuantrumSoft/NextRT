using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Scene;
namespace NextRT.Render
{
    public class SceneRenderer
    {

        public SceneGraph Scene
        {
            get
            {
                return _Scene;
            }
            set
            {
                _Scene = value;
                Sync();
            }
        }
        public SceneGraph _Scene = null;

        public List<NodeCamera> Cams
        {
            get
            {
                return _Cams;
            }
            set
            {
                _Cams = value;
            }
        }
        public List<NodeCamera> _Cams = new List<NodeCamera>();


        public void AddCam(NodeCamera cam)
        {
            _Cams.Add(cam);
        }

        public virtual void Sync()
        {

        }

        public virtual void Render()
        {

        }

    }
}
