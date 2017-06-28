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
        private SettingsPropertyValueCollection _config;
        public Configuration(SettingsPropertyValueCollection config) {
            _config = config;
        }
        private int get(string name)
        {
            return (int)_config[name].PropertyValue;
        }
        private void set(string name, int value)
        {
            _config[name].PropertyValue = value;
        }
        public int PopulationSize
        {
            get { return get("PopulationSize"); }
            set { set("PopulationSize", value); }
        }
        public int Duration
        {
            get { return get("Duration"); }
            set { set("Duration", value); }
        }
        public int MutationRate
        {
            get { return get("MutationRate"); }
            set { set("MutationRate", value); }
        }
        public int CrossoverRate
        {
            get { return get("CrossoverRate"); }
            set { set("CrossoverRate", value); }
        }
        public int SelectionPressure
        {
            get { return get("SelectionPressure"); }
            set { set("SelectionPressure", value); }
        }
    }
}
