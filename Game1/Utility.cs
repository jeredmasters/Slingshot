using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public static class Utility
    {
        public static bool GetBit(this byte b, int bitNumber)
        {
            return (b & (1 << bitNumber)) != 0;
        }
        public static byte Scale(byte a)
        {
            return (byte)Scale((double)a);
        }
        public static int Scale(int a)
        {
            return (int)Scale((double)a);
        } 
        public static float Scale(float a)
        {
            return Scale((double)a);
        }
        public static float Scale(double a)
        {
            return (float)(a * 0.3);
        }
        public static byte NonZero(byte a, byte minimum = 1)
        {
            if (a < minimum)
            {
                return minimum;
            }
            return a;
        }
    }
}
