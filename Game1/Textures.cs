using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Textures
    {
        Texture2D[] _nodes;
        Texture2D[] _muscles;

        public Textures(GraphicsDevice GraphicsDevice)
        {
            _nodes = new Texture2D[255];
            _muscles = new Texture2D[255];

            int x = Helper.Scale(10);
            for (int i = 0; i < 255; i++)
            {
                _nodes[i] = new Texture2D(GraphicsDevice, x, x);
                Color[] colorData2 = new Color[x * x];
                for (int j = 0; j < (x * x); j++)
                    colorData2[j] = new Color(80, (255 - i), 80);

                _nodes[i].SetData<Color>(colorData2);
            }
            for (int i = 0; i < 255; i++)
            {
                _muscles[i] = new Texture2D(GraphicsDevice, x, x);
                Color[] colorData2 = new Color[x * x];
                for (int j = 0; j < (x * x); j++)
                    colorData2[j] = new Color((255 - i), 10, 10);

                _muscles[i].SetData<Color>(colorData2);
            }
        }
        public Texture2D Node(int weight)
        {
            return _nodes[weight];
        }
        public Texture2D Muscle(int strength)
        {
            return _muscles[strength];
        }
    }
}
