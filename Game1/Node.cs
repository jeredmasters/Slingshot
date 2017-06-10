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
        }

        public void SetupGraphics(GraphicsDevice GraphicsDevice)
        {
            Texture = new Texture2D(GraphicsDevice, 10, 10);
            Color[] colorData2 = new Color[10 * 10];
            for (int i = 0; i < 100; i++)
                colorData2[i] = new Color(Weight, 255, Weight);

            Texture.SetData<Color>(colorData2);
        }

        public Texture2D Texture;

        public Vector2 Position;
        public Vector2 Speed;

        public short ID;
        public Int16 Weight;
    }
}
