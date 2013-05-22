using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace BlockConfiguration.GUI
{
    public partial class FastInitForm : Form
    {
        public FastInitForm()
        {
            InitializeComponent();
        }

        public byte NetAddress
        {
            get
            {
                byte num;
                try
                {
                    num = byte.Parse(this.comboBoxNetAddress.Text, NumberStyles.AllowHexSpecifier);
                }
                catch
                {
                    throw new Exception();
                }
                return num;
            }
            set
            {
                this.comboBoxNetAddress.Text = string.Format("{0:X2}",value);
            }
        }

        public bool ParaDelat
        {
            get
            {
                return this.checkBox1.Checked;
            }
        }
    }
}
