using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class GenePool
    {
        public List<Gene> Genes;

        public GenePool(IEnumerable<Gene> genes)
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
