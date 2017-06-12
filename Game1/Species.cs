using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Species
    {
        Breeder _breeder;
        public List<Animal> Animals;
        public Animal Fittest = null;
        GenePool _genepool;
        public int Leaps;
        Vector2 _startingPosition;

        public Species(Vector2 startingPosition, int populationSize)
        {
            _startingPosition = startingPosition;
            _breeder = new Breeder(populationSize);
        }

        public void NewGeneration()
        {
            if (Fittest != null && Fittest.ID != 0)
            {
                Leaps++;
            }
            Animals = new List<Animal>();
            _genepool = _breeder.getNextGeneration();
            foreach (Gene gene in _genepool.Genes)
            {
                Animals.Add(new Animal(gene, _startingPosition, Animals.Count));
            }
            Fittest = null;
        }
    }
}
