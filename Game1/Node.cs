using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Node
    {
        public Node ()
        {
            Speed = new Vector2(0, 0);
            Muscles = new List<Muscle>();
        }

        public List<Muscle> Muscles;

        public Vector2 Offest;
        public Vector2 Position;
        public Vector2 Speed;

        public short ID;
        public Int16 Weight;
    }
}
