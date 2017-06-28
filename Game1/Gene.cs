using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Gene
    {
        public IEnumerable<byte> DNA;
        private int _length;
        public Gene(IEnumerable<byte> dna)
        {
            DNA = dna;
            _length = DNA.Count();
        }

        public byte this[int index]
        {
            get { return DNA.ElementAt(index); }
        }
        public int Length
        {
            get { return _length; }
        }
        public Gene Slice(int from, int count)
        {
            return new Gene(DNA.Skip(from).Take(count));
        }
    }
}
