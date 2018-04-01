using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT;
using NextRT.Lighting;
namespace NextRT.Scene
{
    public class SceneGraph
    {
        public SceneNode Root
        {
            get
            {
                return _Root;
            }
            set
            {
                _Root = value;
            }
        }
        private SceneNode _Root = new SceneNode();

        public List<Light> Lights
        {
            get
            {
                return _Lights;
            }
            set
            {
                _Lights = value;
            }
        }
        public List<Light> _Lights = new List<Light>();

        public int NodeCount
        {
            get
            {
                _NC = 0;
                CountNodes(_Root._Child[0]);
                return _NC;
            }
        }
        public int _NC = 0;
        public void CountNodes(SceneNode node)
        {
            _NC++;
           // Console.WriteLine("NODE:" + node.Name + " C:" + _NC);
            //Console.WriteLine("Node:" + node.Name + " NC:" + nc);
            foreach(var n in node.Child)
            {
                CountNodes(n);
              //  nc = nc + CountNodes(n,nc);
            }
            
        }
        public void Add(Light light)
        {
            _Lights.Add(light);
        }
        public void Add(SceneNode node)
        {

            _Root.Add(node);

        }

    }
}
