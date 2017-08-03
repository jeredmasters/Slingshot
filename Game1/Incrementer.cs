using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Incrementer
    {
        int _value;
        int _min;
        int _max;
        int _increment;

        public Incrementer(int min, int max, int increment)
        {
            _min = min;
            _max = max;
            _increment = increment;

            _value = _min;
        }

        public int Value
        {
            get
            {
                return _value;
            }
        }

        public bool Increment()
        {
            _value += _increment;
            if (_value > _max)
            {
                _value = _min;
                return true;
            }
            if (_value < _min)
            {
                _value = _max;
                return true;
            }
            return false;
        }
    }
}
