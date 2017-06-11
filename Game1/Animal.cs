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
            byte cut3 = 30;
            byte cut2 = (byte)(cut3 + 15);            
            byte cut1 = (byte)(cut2 + 120);
            
            for (short i = 0; i < length; i += 4)
            {                
                if (Gene[i] >= cut1 || i == 0)
                {
                    var subDna = Gene.Slice(i, 4);
                    var node = new Node()
                    {
                        ID = (short)Nodes.Count,
                        Weight = Utility.NonZero(subDna[1],10),
                        Position = startingPosition - new Vector2(Utility.Scale(subDna[2]), Utility.Scale(subDna[3]))
                    };
                    node.SetupGraphics(GraphicsDevice);
                    Nodes.Add(node);
                }
            }
            for (short i = 0; i < length; i += 4)
            {                
                
                if (Gene[i] >= cut2 && Gene[i] < cut1)
                {
                    var subDna = Gene.Slice(i, 4);
                    var muscle = new Muscle()
                    {
                        ID = (short)Muscles.Count,
                        Strength = Utility.NonZero(subDna[1], 10),
                        Length = Utility.NonZero(Utility.Scale(subDna[2]),10),
                        LengthAlpha = Utility.Scale(subDna[2]),
                        NodeP = (byte)(subDna[3] % Nodes.Count),
                        NodeC = (byte)(Muscles.Count % Nodes.Count)
                    };
                    muscle.SetupGraphics(GraphicsDevice);
                    Muscles.Add(muscle);
                }
            }
            for (short i = 0; i < length; i += 4)
            {                
                if (Gene[i] >= cut3 && Gene[i] < cut2)
                {
                    var subDna = Gene.Slice(i, 4);
                    var node = subDna[1] % Nodes.Count;
                    var x = (float)subDna[2] / 200;
                    var y = (float)subDna[3] / 200;
                    Nodes[node].Speed.X = x;
                    Nodes[node].Speed.Y = y;
                }
                if (Gene[i] < cut3)
                {
                    var subDna = Gene.Slice(i, 4);
                    var muscle = subDna[1] % Muscles.Count;
                    Muscles[muscle].OscRange = Utility.NonZero(Utility.Scale(subDna[2]), 10);
                    Muscles[muscle].OscSpeed = (float)subDna[3] / 300;
                }
            }
            foreach (var m in Muscles)
            {
                Nodes[m.NodeC].Muscles.Add(m);
                Nodes[m.NodeP].Muscles.Add(m);
            }
        }
        public int Fitness
        {
            get { return Gene.Fitness; }
            set { Gene.Fitness = value; }
        }
    }
}
