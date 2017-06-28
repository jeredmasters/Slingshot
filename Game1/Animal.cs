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
        public Chromosome Chromosome;
        public List<Node> Nodes = new List<Node>();
        public List<Muscle> Muscles = new List<Muscle>();
        public int ID;
        public bool Retard;

        public Animal(Chromosome chromosome, Vector2 startingPosition, int id)
        {
            ID = id;
            Chromosome = chromosome;
            Fitness = 0;
            int length = Chromosome.Length - 4;
            Retard = false;

            for (short i = 0; i < length; i += 4)
            {                
                if (Helper.ReadGene(Chromosome[i]) == Feature.Node)
                {
                    var subDna = Chromosome.Slice(i, 4);
                    var node = new Node()
                    {
                        ID = (short)Nodes.Count,
                        Weight = Helper.NonZero(subDna[1],10),
                        Position = startingPosition - new Vector2(Helper.Scale(subDna[2]), Helper.Scale(subDna[3]))
                    };
                    Nodes.Add(node);
                }
            }
            if (Nodes.Count == 0)
            {
                Retard = true;
                Muscles.Clear();
                return;
            }
            for (short i = 0; i < length; i += 4)
            {
                if (Helper.ReadGene(Chromosome[i]) == Feature.Muscle)
                {
                    var subDna = Chromosome.Slice(i, 4);
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
            if (Muscles.Count == 0)
            {
                Retard = true;
                Nodes.Clear();
                return;
            }
            for (short i = 0; i < length; i += 4)
            {
                if (Helper.ReadGene(Chromosome[i]) == Feature.NodeSpeed)
                {
                    var subDna = Chromosome.Slice(i, 4);
                    var node = subDna[1] % Nodes.Count;
                    var x = (float)subDna[2] / 100;
                    var y = (float)subDna[3] / 100;
                    Nodes[node].Velocity.X = x;
                    Nodes[node].Velocity.Y = y;
                }
                if (Helper.ReadGene(Chromosome[i]) == Feature.MuscleOscillation)
                {
                    var subDna = Chromosome.Slice(i, 4);
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
            foreach(var n in Nodes)
            {
                if (n.Muscles.Count == 0)
                {
                    Retard = true;
                }
            }
        }
        public int Fitness
        {
            get { return Chromosome.Fitness; }
            set
            {
                if (!Retard)
                {
                    Chromosome.Fitness = value;
                }
            }
        }
        public string Species
        {
            get { return Chromosome.Species();  }
        }
    }
}
