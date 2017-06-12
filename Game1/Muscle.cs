using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Muscle : IDisposable
    {
        public Muscle()
        {
            OscRange = 0;
            OscState = 0;
            OscDirection = false;
        }
        public void SetupGraphics(GraphicsDevice GraphicsDevice)
        {
            Texture = new Texture2D(GraphicsDevice, 10, 10);
            Color[] colorData2 = new Color[10 * 10];
            Color = new Color((255 - Strength), 10, 10);
            for (int i = 0; i < 100; i++)
                colorData2[i] = Color;

            Texture.SetData<Color>(colorData2);
        }

        public void Dispose()
        {
            Texture.Dispose();
        }

        public Texture2D Texture;

        public byte Strength;
        public float Length;
        public byte LengthAlpha;
        public byte NodeP;
        public byte NodeC;
        public Color Color;

        public Vector2 PosP;
        public Vector2 PosC;

        public short ID;

        public byte OscRange;
        public float OscSpeed;
        public float OscState;
        public bool OscDirection;

    }
}
