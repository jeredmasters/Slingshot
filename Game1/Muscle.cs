using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Muscle
    {
        public Muscle()
        {

        }
        public void SetupGraphics(GraphicsDevice GraphicsDevice)
        {
            Texture = new Texture2D(GraphicsDevice, 10, 10);
            Color[] colorData2 = new Color[10 * 10];
            for (int i = 0; i < 100; i++)
                colorData2[i] = new Color(Strength, 255, Strength);

            Texture.SetData<Color>(colorData2);
        }
        public Texture2D Texture;

        public Int16 Strength;
        public Int16 LengthAlpha;
        public Int16? Length = null;
        public Int16 NodeP;
        public Int16 NodeC;

        public Vector2 PosP;
        public Vector2 PosC;

        public short ID;
        
    }
}
