using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Render
{
    public class SimpleRenderer : SceneRenderer
    {

        public override void Render()
        {
            Console.WriteLine("RenderScene. Cams:" + _Cams.Count + " Nodes:" + Scene.NodeCount);
        }

    }
}
