using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Scene;
using Assimp;
using NextRT.Data;
using Assimp.Unmanaged;
using Assimp.Configs;
namespace NextRT.Import
{
    public class ImportAssImp : Importer
    {
        public override NodeEntity Import(string path)
        {

            NodeEntity root = new NodeEntity();

            var ac = new AssimpContext();

            ac.SetConfig(new NormalSmoothingAngleConfig(66.0f));
            
            var scene = ac.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.CalculateTangentSpace | PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.ValidateDataStructure);

            var ml = scene.Meshes;

            var lm = new List<MeshData>();

            foreach(var m in ml)
            {


                int vi = 0;

                List<Vertex> vl = new List<Vertex>();

                var nm = new MeshData(m.FaceCount);

                MD.Add(nm);

                lm.Add(nm);

                nm.Name = m.Name;
          

                foreach(var v in m.Vertices)
                {

                    var mv = new Vertex();
                    vl.Add(mv);

                    mv.X = v.X;
                    mv.Y = v.Y;
                    mv.Z = v.Z;

                    mv.NX = m.Normals[vi].X;
                    mv.NY = m.Normals[vi].Y;
                    mv.NZ = m.Normals[vi].Z;

                    mv.U = m.TextureCoordinateChannels[0][vi].X;
                    mv.V = m.TextureCoordinateChannels[0][vi].Y;
                    mv.W = m.TextureCoordinateChannels[0][vi].Z;

                    mv.BIX = m.BiTangents[vi].X;
                    mv.BIY = m.BiTangents[vi].Y;
                    mv.BIZ = m.BiTangents[vi].Z;

                    mv.TX = m.Tangents[vi].X;
                    mv.TY = m.Tangents[vi].Y;
                    mv.TZ = m.Tangents[vi].Z;

                    if (m.HasVertexColors(0))
                    {

                        mv.R = m.VertexColorChannels[0][vi].R;
                        mv.G = m.VertexColorChannels[0][vi].G;
                        mv.B = m.VertexColorChannels[0][vi].B;
                        mv.A = m.VertexColorChannels[0][vi].A;
                    }

                    vi++;

                }

                int ti = 0;
            
                foreach(var f in m.Faces)
                {

                    var mt = new MeshTri();

                    mt.Vertices[0] = vl[f.Indices[0]];
                    mt.Vertices[1] = vl[f.Indices[1]];
                    mt.Vertices[2] = vl[f.Indices[2]];
                    nm.Tris[ti] = mt;

                    ti++;

                }

                Console.WriteLine("MeshName:" + nm.Name + " Verts:" + vl.Count + " Tris:" + nm.Tris.Length);

            }

            root.Meshes = lm;

               
            ParseNode(scene.RootNode,root,1);

            

            return root;


        
        }

        List<MeshData> MD = new List<MeshData>();


        public void ParseNode(Node node,NodeEntity rnode,int na=1)
        {
            Console.WriteLine("Node:" + node.Name+" NC:"+na);

            var nm = node.Transform;

            rnode.Name = node.Name;

            if (node.HasMeshes)
            {
                foreach (var mi in node.MeshIndices)
                {
                    rnode.Meshes.Add(MD[mi]);
                    Console.WriteLine("Mesh:"+MD[mi].Name);
                }
                Console.WriteLine("Had " + rnode.Meshes.Count + " meshes");
            }
            foreach(var cn in node.Children)
            {
                var nn = new NodeEntity();

                nn.Root = rnode;
                rnode.Add(nn);

                ParseNode(cn,nn,na+1);
            }
        }
    }
}
