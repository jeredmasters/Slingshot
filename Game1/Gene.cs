using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Gene
    {
        public IEnumerable<byte> DNA;
        public int Fitness;
        public Gene(IEnumerable<byte> dna)
        {
            DNA = dna;
        }

        public byte this[int index]
        {
            get { return DNA.ElementAt(index); }
        }
        public int Length()
        {
            return DNA.Count();
        }
        public Gene Slice(int from, int count)
        {
            return new Gene(DNA.Skip(from).Take(count));
        }
    }
}
