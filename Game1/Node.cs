using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Node
    {
        public Node ()
        {
            Speed = new Vector2(0, 0);
            Muscles = new List<Muscle>();
        }

        public void SetupGraphics(GraphicsDevice GraphicsDevice)
        {
            int x = Utility.Scale(10);
            Texture = new Texture2D(GraphicsDevice, x, x);
            Color[] colorData2 = new Color[x * x];
            for (int i = 0; i < (x * x); i++)
                colorData2[i] = new Color(80, (255 - Weight), 80);

            Texture.SetData<Color>(colorData2);
        }

        public List<Muscle> Muscles;

        public Texture2D Texture;

        public Vector2 Position;
        public Vector2 Speed;

        public short ID;
        public Int16 Weight;
    }
}
