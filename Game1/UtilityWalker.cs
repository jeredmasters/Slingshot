using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class UtilityWalker : IUtility
    {
        private int _floor;
        public UtilityWalker(int floor)
        {
            _floor = floor;
        }
        public Animal Evaluate(IEnumerable<Animal> animals)
        {
            Animal fittest = null;
            foreach (var animal in animals)
            {
                bool onGround = false;
                foreach (var node in animal.Nodes)
                {
                    if (node.Position.Y >= _floor)
                    {
                        onGround = true;
                    }
                    var fitness = (int)((_floor - node.Position.Y) * (node.Position.X - 100));
                    if (fitness > animal.Fitness)
                    {
                        animal.Fitness = fitness;
                    }
                }
                if (!onGround)
                {
                    animal.Fitness = 0;
                }
                if (fittest == null || animal.Fitness > fittest.Fitness)
                {
                    fittest = animal;
                }
            }
            return fittest;
        }
    }
}
