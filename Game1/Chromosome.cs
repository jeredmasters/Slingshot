using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Chromosome : Gene
    {
        public int Fitness { get; set; }
        public Chromosome(IEnumerable<byte> dna) : base(dna)
        {
        }

        public string Species()
        {
            string species = "";
            for(int i = 0; i < this.Length - 4; i += 4)
            {
                var f = Helper.ReadGene(DNA.ElementAt(i));
                switch (f)
                {
                    case Feature.Muscle:
                        species += "M";
                        break;
                    case Feature.Node:
                        species += "N";
                        break;
                    case Feature.NodeSpeed:
                        species += "S";
                        break;
                    case Feature.MuscleOscillation:
                        species += "O";
                        break;
                }
            }
            return species;
        }

        public override string ToString()
        {
            return Convert.ToBase64String(DNA.ToArray());
        }
    }
}
