using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Configuration
    {
        private Dictionary<string, int> _config;
        public Configuration() {
            _config = new Dictionary<string, int>();
            PopulationSize = 1000;
            SelectionPressure = 100;
        }
        public Configuration(Dictionary<string, int> config)
        {
            _config = config;
        }
        private int get(string name)
        {
            return _config[name];
        }
        private void set(string name, int value)
        {
            _config[name] = value;
        }
        public int PopulationSize
        {
            get { return get("PopulationSize"); }
            set { set("PopulationSize", value); }
        }
        public int SelectionPressure
        {
            get { return get("SelectionPressure"); }
            set { set("SelectionPressure", value); }
        }
    }
}
