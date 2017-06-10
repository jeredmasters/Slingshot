using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Animal
    {
        public Gene Gene;
        public List<Node> Nodes = new List<Node>();
        public List<Muscle> Muscles = new List<Muscle>();

        public Animal(Gene gene, GraphicsDevice GraphicsDevice, Vector2 startingPosition)
        {
            Gene = gene;
            int length = Gene.Length() - 4;
            int cut = 180;
            for(short i = 0; i < length; i += 4)
            {
                var subDna = Gene.Slice(i,4);
                Int16 type = (Int16)subDna[0];
                if (type > cut || i == 0)
                {
                    var node = new Node()
                    {
                        ID = (short)Nodes.Count,
                        Weight = subDna[1],
                        Position = startingPosition - new Vector2(subDna[2], subDna[3])
                    };
                    node.SetupGraphics(GraphicsDevice);
                    Nodes.Add(node);
                }
            }
            for (short i = 0; i < length; i += 4)
            {
                
                var subDna = Gene.Slice(i,4);
                Int16 type = (Int16)subDna[0];
                if (type <= cut && subDna[1] > 0)
                {
                    var muscle = new Muscle()
                    {
                        ID = (short)Muscles.Count,
                        Strength = subDna[1],
                        Length = subDna[2],
                        NodeP = (byte)(subDna[3] % Nodes.Count),
                        NodeC = (byte)(Muscles.Count % Nodes.Count)
                    };
                    muscle.SetupGraphics(GraphicsDevice);
                    Muscles.Add(muscle);
                }
            }
        }
        public int Fitness
        {
            get { return Gene.Fitness; }
            set { Gene.Fitness = value; }
        }
    }
}
