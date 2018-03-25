using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Data
{
    public class MeshData
    {
        public Identity ID
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
        public Identity _ID = new Identity("");

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

        public MeshTri[] Tris
        {
            get
            {
                return _Tris;
            }
            set
            {
                _Tris = value;
            }
        }
        public MeshTri[] _Tris = null;

        public MeshData(int tris=0)
        {
            _Tris = new MeshTri[tris];
        }

    }
}
