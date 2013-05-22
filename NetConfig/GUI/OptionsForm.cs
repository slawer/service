using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetConfig.GUI
{
    public partial class OptionsForm : Form
    {
        public OptionsForm(StatusHandle han)
        {
            InitializeComponent();
            handle = han;
        }

        private StatusHandle handle = null;

        private void accept_Click(object sender, EventArgs e)
        {
            if (handle != null)
            {
                if (comboBox1.SelectedIndex == 0) handle.Algorithm = UsedAlgorithm.Cucliced;
                else handle.Algorithm = UsedAlgorithm.Broadcast;

                handle.Interval = (int)numericUpDown1.Value;
            }
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            if (handle.Algorithm == UsedAlgorithm.Cucliced) comboBox1.SelectedIndex = 0;
            else comboBox1.SelectedIndex = 1;

            numericUpDown1.Value = handle.Interval;
        }
    }
}
