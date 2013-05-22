using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlockConfiguration.GUI
{
    public partial class EditOprosCmdForm : Form
    {
        public EditOprosCmdForm()
        {
            InitializeComponent();
        }

        private void EditOprosCmdForm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 29; i++)
            {
                comboBoxSize.Items.Add(string.Format("{0:X2}", i));
            }
        }
    }
}