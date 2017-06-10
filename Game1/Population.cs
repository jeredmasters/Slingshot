using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Population
    {
        public List<Gene> Genes;

        public Population(IEnumerable<Gene> genes)
        {
            Genes = genes.ToList();
        }

        public int Size
        {
            get
            {
                return Genes.Count;
            }
        }
    }
}
