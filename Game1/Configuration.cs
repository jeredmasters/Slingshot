using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Slingshot
{
    public class Configuration
    {
        public Configuration() {
        }
        public Incrementer PopulationSize;
        public Incrementer Duration;
        public Incrementer MutationRate;
        public Incrementer CrossoverRate;
        public Incrementer Complexity;
        public Incrementer SelectionPressure;

        public void Increment()
        {
            if (Complexity.Increment())
            {
                if (CrossoverRate.Increment())
                {
                    if (MutationRate.Increment())
                    {
                        if (SelectionPressure.Increment())
                        {
                            if (PopulationSize.Increment())
                            {
                                Duration.Increment();
                            }
                        }
                    }
                }
            }
        }

        public Dictionary<string, string> getStats()
        {
            Dictionary<string, string> retval = new Dictionary<string, string>();
            retval.Add("Complexity: ", Complexity.Value.ToString());
            retval.Add("CrossoverRate: ", CrossoverRate.Value.ToString());
            retval.Add("MutationRate: ", MutationRate.Value.ToString());
            retval.Add("SelectionPressure: ", SelectionPressure.Value.ToString());
            retval.Add("PopulationSize: ", PopulationSize.Value.ToString());
            retval.Add("Duration: ", Duration.Value.ToString());

            return retval;
        }
    }
}
