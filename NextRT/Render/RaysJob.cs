using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextRT.Job;
namespace NextRT.Render
{
    public class RaysJob : ThreadJob
    {
        public static float[] RayData;
        public static Scene.NodeCamera Cam;
        public static System.Threading.Mutex RayM = new System.Threading.Mutex();

        public override void Update()
        {
            var rh = Obj as RayHold;
            // Console.WriteLine("RS:" + rh.RayStart + " Num:" + rh.RayNum);
            int aa = 0;
            var cp = Cam.Position;
            var cm = Cam.Rotation;
            var pv = new OpenTK.Vector3(0, 0, 500);
            for (int i=rh.RayStart;i<rh.RayStart+rh.RayNum;i++)
            {
                
                OpenTK.Vector3 rayS = Cam.Transform(rh.aX[aa], rh.aY[aa], 0);
                var ai = i * 6;
                //  Console.WriteLine("ax:" + ax + " aY:" + ay);
                RayData[ai] = cp.X+ rayS.X;
                RayData[ai + 1] = cp.Y + rayS.Y;
                RayData[ai + 2] = cp.Z + rayS.Z;
                // Console.WriteLine("RX:" + RayData[ray] + " RY:" + RayData[ray + 1] + " RZ:" + RayData[ray + 2]);
                

                OpenTK.Vector3 fv = -OpenTK.Vector3.TransformPosition(pv, cm);
                RayData[ai+ 3] = fv.X;
                RayData[ai + 4] = fv.Y;
                RayData[ai + 5] = fv.Z;

                             

                aa++;
            }
            //Console.WriteLine("Complete!");

            RoundComplete = true;


        }

    }
}
