﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public static class Helper
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
        public static Feature ReadGene(byte b)
        {
            byte cut3 = 30;
            byte cut2 = (byte)(cut3 + 15);
            byte cut1 = (byte)(cut2 + 120);
            if (b >= cut1)
            {
                return Feature.Node;
            }
            if (b >= cut2)
            {
                return Feature.Muscle;
            }
            if (b >= cut3)
            {
                return Feature.NodeSpeed;
            }
            return Feature.MuscleOscillation;
        }
    }
    public enum Feature
    {
        Muscle,
        Node,
        NodeSpeed,
        MuscleOscillation
    }
}
