using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Inputs
{
    public class Input
    {
        public static float MX
        {
            get;
            set;
        }
        public static float MY
        {
            get;
            set;
        }
        public static float MZ
        {
            get;
            set;
        }
        public static float MXD
        {
            get;
            set;

        }
        public static float MYD
        {
            get;
            set;
        }
        public static float MZD
        {
            get;
            set;
        }
        public static bool[] But = new bool[32];
    }
}
