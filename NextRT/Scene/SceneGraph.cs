using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT;
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

        public void Add(SceneNode node)
        {

            _Root.Add(node);

        }

    }
}
