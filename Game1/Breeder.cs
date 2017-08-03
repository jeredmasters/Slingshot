using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    class Breeder : IDisposable
    {
        Random _rnd = new Random();
        int _generation = 0;
        List<Chromosome> _genePool = null;
        int _size = 200;
        int _mutationRate = 100; // 0 - 1000
        int _crossover = 10; // 0 - 100
        int _dnaMin = 10;
        int _dnaMax = 30;
        int _selectionPressure = 10;

        /// <summary>
        /// Initialize the breeder
        /// </summary>
        /// <param name="populationSize"></param>
        /// <param name="mutationRate">0 - 1000</param>
        /// <param name="crossover">0 - 100</param>
        public Breeder(int complexity, int populationSize, int mutationRate, int crossover, int selectionPressure)
        {
            _dnaMin = complexity;
            _dnaMax = (int)(complexity * 1.5);
            _size = populationSize;
            _mutationRate = mutationRate;
            _crossover = crossover;
            _selectionPressure = selectionPressure;
        }


        private IEnumerable<byte> getDna()
        {
            return getDnaChunk(_dnaMin, _dnaMax);
        }

        private IEnumerable<byte> getDnaChunk(int from, int to)
        {
            int length = _rnd.Next(from, to) * 4;
            byte[] dna = new byte[length];
            for (int i = 0; i < length; i++)
            {
                dna[i] = (byte)_rnd.Next(10, 255);
            }
            return dna;
        }

        private IEnumerable<Chromosome> createPopulation(int l)
        {
            _generation = 1;
            for (int i = 0; i < l; i++)
            {
                yield return new Chromosome(getDna());
            }
        }

        public List<Chromosome> getNextGeneration()
        {
            _generation++;
            if (_genePool == null)
            {
                _genePool = createPopulation(_size).ToList();
            }
            else
            {
                _genePool = mutate().ToList();
            }
            return _genePool;
        }
        private IEnumerable<Chromosome> mutate()
        {
            Chromosome best = null;
            foreach(var g in _genePool)
            {
                if (best == null || g.Fitness > best.Fitness)
                {
                    best = g;
                }
            }
            yield return best;
            if (best != null && best.Fitness > 0)
            {
                List<int> indexWheel = new List<int>();
                for (int i = 0; i < _genePool.Count; i++)
                {
                    int value = (((_genePool.ElementAt(i).Fitness - (best.Fitness / (20 / _selectionPressure))) * (_size / 5)) / best.Fitness);

                    for (int j = 0; j < value; j++)
                    {
                        indexWheel.Add(i);
                    }
                }
                for (int i = 0; i < _size; i++)
                {
                    var a = _genePool.ElementAt(indexWheel[_rnd.Next(0, indexWheel.Count)]);
                    var b = _genePool.ElementAt(indexWheel[_rnd.Next(0, indexWheel.Count)]);
                    yield return Copulate(a, b);
                }
                for (int i = 0; i < 5; i++)
                {
                    yield return new Chromosome(getDna());
                }
            }
            else
            {
                for (int i = 0; i < _size; i++)
                {
                    yield return new Chromosome(getDna());
                }
            }

        }
        private Chromosome Copulate(Chromosome a, Chromosome b)
        {
            if (_crossover == 0)
            {
                return new Chromosome(Mutate(a.DNA.ToArray()));
            }
            int length = (a.Length + b.Length) / 2;
            bool sw = false;
            byte[] c = new byte[length];
            for(int i = 0; i < length; i++)
            {
                if(_rnd.Next(0,100 / _crossover) == 0)
                {
                    sw = !sw;
                }
                if ((sw && a.Length >= length) || (b.Length < length))
                {
                    c[i] = a[i];
                }
                else
                {
                    c[i] = b[i];
                }
            }
            return new Chromosome(Mutate(c));
        }
        private IEnumerable<byte> Mutate(byte[] g)
        {
            for (int i = 0; i < g.Count(); i++)
            {
                if (_rnd.Next(0, _mutationRate / 2) == 0)
                {
                    g[i] = (byte)(Math.Abs(g[i] + _rnd.Next(-127, 127)) % 255); 
                }
            }
            if (_rnd.Next(0, _mutationRate / 5) == 0)
            {
                int position = _rnd.Next(0, g.Count() / 4) * 4;
                List<byte> n = new List<byte>(g);
                n.InsertRange(position, getDnaChunk(1, 3));
                return n;
            }
            if (_rnd.Next(0, _mutationRate / 5) == 0)
            {
                int position = _rnd.Next(0, g.Count() / 4) * 4;
                List<byte> n = new List<byte>(g);
                n.RemoveRange(position, 4);
                return n;
            }
            if (_rnd.Next(0, _mutationRate/5) == 0)
            {
                int position = _rnd.Next(0, g.Count() / 4) * 4;
                List<byte> n = new List<byte>(g);
                var p = n.Skip(position).Take(4).ToArray();
                n.InsertRange(position, p);
                return n;
            }
            return g;
        }

        public void Dispose()
        {
            _genePool.Clear();
        }

        public int Generation
        {
            get { return _generation; }
        }
    }
}
