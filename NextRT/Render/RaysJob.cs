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
            for (int i=rh.RayStart;i<rh.RayStart+rh.RayNum;i++)
            {
                float ax, ay;
                ax = rh.aX[aa];
                ay = rh.aY[aa];
                OpenTK.Vector3 rayS = Cam.Transform(ax, ay, 0);
                var ai = i * 6;
                //  Console.WriteLine("ax:" + ax + " aY:" + ay);
                RayData[ai] = Cam.Position.X + rayS.X;
                RayData[ai + 1] = Cam.Position.Y + rayS.Y;
                RayData[ai + 2] = Cam.Position.Z + rayS.Z;
                // Console.WriteLine("RX:" + RayData[ray] + " RY:" + RayData[ray + 1] + " RZ:" + RayData[ray + 2]);
                float fx, fy, fz;
                OpenTK.Vector3 fv = -Cam.Transform(0, 0, 500);

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
