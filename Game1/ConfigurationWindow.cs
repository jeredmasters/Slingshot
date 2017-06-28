using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slingshot
{
    public partial class ConfigurationWindow : Form
    {
        public ConfigurationWindow()
        {
            InitializeComponent();
            
            numPopulationSize.Value = Properties.Settings.Default.PopulationSize;
            numDuration.Value = Properties.Settings.Default.Duration;
            numMutationRate.Value = Properties.Settings.Default.MutationRate;
            numCrossoverRate.Value = Properties.Settings.Default.CrossoverRate;
            numSelectionPressure.Value = Properties.Settings.Default.SelectionPressure;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PopulationSize = (int)numPopulationSize.Value;
            Properties.Settings.Default.Duration = (int)numDuration.Value;
            Properties.Settings.Default.MutationRate = (int)numMutationRate.Value;
            Properties.Settings.Default.CrossoverRate = (int)numCrossoverRate.Value;
            Properties.Settings.Default.SelectionPressure = (int)numSelectionPressure.Value;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
