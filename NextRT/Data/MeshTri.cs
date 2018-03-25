using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Data
{
    public class MeshTri
    {
        public Vertex[] Vertices = new Vertex[3];
        public MeshTri()
        {
            Vertices[0] = new Vertex();
            Vertices[1] = new Vertex();
            Vertices[2] = new Vertex();
        }
    }
}
