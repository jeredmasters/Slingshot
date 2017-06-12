using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    interface IUtility
    {
        Animal Evaluate(IEnumerable<Animal> animals);
    }
}
