using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Animal
    {
        public Gene Gene;
        public List<Node> Nodes = new List<Node>();
        public List<Muscle> Muscles = new List<Muscle>();
        public int ID;

        public Animal(Gene gene, Vector2 startingPosition, int id)
        {
            ID = id;
            Gene = gene;
            Fitness = 0;
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
                        Weight = Helper.NonZero(subDna[1],10),
                        Position = startingPosition - new Vector2(Helper.Scale(subDna[2]), Helper.Scale(subDna[3]))
                    };
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
                        Strength = Helper.NonZero(subDna[1], 10),
                        Length = Helper.NonZero(Helper.Scale(subDna[2]),10),
                        LengthAlpha = Helper.Scale(subDna[2]),
                        NodeP = (byte)(subDna[3] % Nodes.Count),
                        NodeC = (byte)(Muscles.Count % Nodes.Count)
                    };
                    Muscles.Add(muscle);
                }
            }
            for (short i = 0; i < length; i += 4)
            {                
                if (Gene[i] >= cut3 && Gene[i] < cut2)
                {
                    var subDna = Gene.Slice(i, 4);
                    var node = subDna[1] % Nodes.Count;
                    var x = (float)subDna[2] / 100;
                    var y = (float)subDna[3] / 100;
                    Nodes[node].Speed.X = x;
                    Nodes[node].Speed.Y = y;
                }
                if (Gene[i] < cut3)
                {
                    var subDna = Gene.Slice(i, 4);
                    var muscle = subDna[1] % Muscles.Count;
                    Muscles[muscle].OscRange = Helper.NonZero(Helper.Scale(subDna[2]), 10);
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
