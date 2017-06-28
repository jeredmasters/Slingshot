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
        List<Chromosome> _genepool;
        Vector2 _startingPosition;
        int _leaps = 0;

        public Species(Vector2 startingPosition, Configuration configuration)
        {
            _startingPosition = startingPosition;
            _breeder = new Breeder(configuration.PopulationSize, configuration.MutationRate, configuration.CrossoverRate, configuration.SelectionPressure);
        }

        public void NewGeneration()
        {
            if (Fittest != null && Fittest.ID != 0)
            {
                _leaps++;
            }
            Animals = new List<Animal>();
            _genepool = _breeder.getNextGeneration();
            foreach (Chromosome gene in _genepool)
            {
                Animals.Add(new Animal(gene, _startingPosition, Animals.Count));
            }
            Fittest = null;
        }

        public int Generation
        {
            get { return _breeder.Generation; }
        }
        public int Leaps
        {
            get { return _leaps; }
        }
    }
}
