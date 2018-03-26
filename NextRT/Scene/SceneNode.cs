using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT;
namespace NextRT.Scene
{
    public class SceneNode
    {
        public List<SceneNode> Child
        {
            get
            {
                return _Child;
            }
            set
            {
                _Child = value;
            }
        }
        public List<SceneNode> _Child = new List<SceneNode>();

        public Data.Identity ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        public Data.Identity _ID = new Data.Identity("");

        public string Name
        {
            get
            {
                return _ID.Name;
            }
            set
            {
                _ID.Name = value;
            }
        }

        public void Add(SceneNode node)
        {
            _Child.Add(node);
        }

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
        public SceneNode _Root = null;

    }
}
