using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Statistics
    {
        public int Generation;
        public List<GenerationStat> GenStat;

        public Statistics()
        {
            Generation = 0;
            GenStat = new List<GenerationStat>();
        }
    }
    public class GenerationStat
    {
        public float AvgInitDelta;
        public float AvgInitForce;
        public int Size;
        public int ID;

        public GenerationStat(int id, int size)
        {
            ID = id;
            Size = size;
            AvgInitDelta = 0;
            AvgInitForce = 0;
        }  
    }
}
