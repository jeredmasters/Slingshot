using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Slingshot
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Simulation : IDisposable
    {
        Configuration _config;
        Physics _physics;
        IUtility _utility;
        Storage _storage;

        Breeder _breeder;
        List<Animal> _animals;
        Animal _fittest = null;
        List<Chromosome> _genepool;
        int _leaps = 0;
        int _id;


        int _rate;
        int _clicks = int.MaxValue;

        const int WindowHeight = 1000;
        const int WindowWidth = 1800;
        const int Floor = WindowHeight - 100;
        const int startline = 200;
        
        Vector2 _startingPosition = new Vector2(startline, Floor);
               

        public Simulation(int id, Configuration config, int rate, Storage storage = null)
        {
            _id = id;
            _rate = rate;
            _config = config;
            _storage = storage;
            _physics = new Physics(Floor, WindowWidth, WindowHeight);
            _utility = new UtilityWalker(Floor);
            _breeder = new Breeder(
                _config.Complexity.Value,
                _config.PopulationSize.Value, 
                _config.MutationRate.Value, 
                _config.CrossoverRate.Value, 
                _config.SelectionPressure.Value);
            
        }
        
        public int Click()
        {            
            if (_clicks >= _config.Duration.Value)
            {
                NewGeneration();
            }
            for(int i = 0; i < _rate; i++)
            {
                _clicks++;
                _physics.ProcessPhysics(_animals);
                _fittest = _utility.Evaluate(_animals);
            }
            return Generation;
        }     
        
        public int Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                _rate = value;
                if (_rate < 1)
                {
                    _rate = 1;
                }
                if (_rate > 30)
                {
                    _rate = 30;
                }
            }
        }
        
        public Dictionary<string,string> getStats()
        {
            Dictionary<string, string> retval = new Dictionary<string, string>();
            retval.Add("Generation: ", Generation.ToString());
            retval.Add("Rate: ", _rate.ToString());
            retval.Add("Clicks: ", _clicks.ToString());
            retval.Add("Fittest: ", (_fittest == null ? "n/a" : _fittest.ID.ToString() + "("+_fittest.Fitness+")" + "(" + _fittest.Species +")"));
            retval.Add("Leaps: ", Leaps.ToString());
            retval.Add("Clipping: ", _physics.Clipping.ToString());

            return retval;
        }

        public void NewGeneration()
        {
            _clicks = 0;
            if (_fittest != null && _fittest.ID != 0)
            {
                _leaps++;
            }
            if (_storage != null && _fittest != null)
            {
                _storage.SaveGeneration(_id, _breeder.Generation, _fittest);
            }
            _animals = new List<Animal>();
            _genepool = _breeder.getNextGeneration();
            foreach (Chromosome gene in _genepool)
            {
                _animals.Add(new Animal(gene, _startingPosition, _animals.Count));
            }
            _physics.Initialize(_animals);
            _fittest = null;
        }

        public void Dispose()
        {
            _genepool.Clear();
            _animals.Clear();
            _breeder.Dispose();
        }

        public int Generation
        {
            get { return _breeder.Generation; }
        }
        public int Leaps
        {
            get { return _leaps; }
        }
        public List<Animal> Animals
        {
            get
            {
                return _animals;
            }
        }
        public Animal Fittest
        {
            get
            {
                return _fittest;
            }
        }
        public int Id
        {
            get
            {
                return _id;
            }
        }
    }
}
