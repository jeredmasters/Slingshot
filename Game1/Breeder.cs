using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Breeder
    {
        Random rnd = new Random();
        Population population = null;
        int size = 200;

        private IEnumerable<byte> getDNA(int from = 20, int to = 60)
        {
            int length = rnd.Next(from, to) * 4;
            byte[] dna = new byte[length];
            for (int i = 0; i < length; i++)
            {
                dna[i] = (byte)rnd.Next(10, 255);
            }
            return dna;
        }

        private IEnumerable<Gene> createPopulation(int l)
        {
            for (int i = 0; i < l; i++)
            {
                yield return new Gene(getDNA());
            }
        }

        public Population getNextGeneration()
        {
            if (population == null)
            {
                population = new Population(createPopulation(size));
            }
            else
            {
                population = new Population(mutate());
            }
            return population;
        }
        private IEnumerable<Gene> mutate()
        {
            Gene best = null;
            foreach(var g in population.Genes)
            {
                if (best == null || g.Fitness > best.Fitness)
                {
                    best = g;
                }
            }
            yield return best;
            if (best != null && best.Fitness > 0)
            {
                List<int> indexWheel = new List<int>();
                for (int i = 0; i < population.Size; i++)
                {
                    int value = (((population.Genes.ElementAt(i).Fitness - (best.Fitness / 2)) * (size / 5)) / best.Fitness);

                    for (int j = 0; j < value; j++)
                    {
                        indexWheel.Add(i);
                    }
                }
                for (int i = 0; i < size; i++)
                {
                    var a = population.Genes.ElementAt(indexWheel[rnd.Next(0, indexWheel.Count)]);
                    var b = population.Genes.ElementAt(indexWheel[rnd.Next(0, indexWheel.Count)]);
                    yield return Copulate(a, b);
                }
                for (int i = 0; i < 5; i++)
                {
                    yield return new Gene(getDNA());
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    yield return new Gene(getDNA());
                }
            }

        }
        private Gene Copulate(Gene a, Gene b)
        {
            int length = (a.Length() + b.Length()) / 2;
            bool sw = false;
            byte[] c = new byte[length];
            for(int i = 0; i < length; i++)
            {
                if(rnd.Next(0,10) == 0)
                {
                    sw = !sw;
                }
                if ((sw && a.Length() >= length) || (b.Length() < length))
                {
                    c[i] = a[i];
                }
                else
                {
                    c[i] = b[i];
                }
            }
            return new Gene(Mutate(c));
        }
        private IEnumerable<byte> Mutate(byte[] g)
        {
            for (int i = 0; i < g.Count(); i++)
            {
                if (rnd.Next(0,20) == 0)
                {
                    g[i] = (byte)(Math.Abs(g[i] + rnd.Next(-127, 127)) % 255); 
                }
            }
            if (rnd.Next(0, 15) == 0)
            {
                int position = rnd.Next(0, g.Count() / 4) * 4;
                List<byte> n = new List<byte>(g);
                n.InsertRange(position, getDNA(1, 3));
                return n;
            }
            if (rnd.Next(0, 15) == 0)
            {
                int position = rnd.Next(0, g.Count() / 4) * 4;
                List<byte> n = new List<byte>(g);
                n.RemoveRange(position, 4);
                return n;
            }
            if (rnd.Next(0, 15) == 0)
            {
                int position = rnd.Next(0, g.Count() / 4) * 4;
                List<byte> n = new List<byte>(g);
                var p = n.Skip(position).Take(4).ToArray();
                n.InsertRange(position, p);
                return n;
            }
            return g;
        }
    }
}
