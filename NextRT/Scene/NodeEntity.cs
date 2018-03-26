using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Data;
namespace NextRT.Scene
{
    public class NodeEntity : Node3D
    {

        public List<MeshData> Meshes
        {
            get
            {
                return _Meshes;
            }
            set
            {
                _Meshes = value;
            }
        }
        public List<MeshData> _Meshes = new List<MeshData>();




    }
}
