using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Muscle
    {
        public Muscle()
        {
            OscRange = 0;
            OscState = 0;
            OscDirection = false;
        }
        

        public byte Strength;
        public float Length;
        public byte LengthAlpha;
        public byte NodeP;
        public byte NodeC;

        public Vector2 PosP;
        public Vector2 PosC;

        public short ID;

        public byte OscRange;
        public float OscSpeed;
        public float OscState;
        public bool OscDirection;

    }
}
