using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using BlockConfiguration.IO;

namespace BlockConfiguration.GUI
{
    public partial class OptionsOfBlockForm : Form
    {
        public OptionsOfBlockForm()
        {
            InitializeComponent();
        }

        Block block = null;
        public Block Block
        {
            get { return block; }
            set
            {
                block = value;
                InsertBlock();
            }
        }

        void InsertBlock()
        {
            comboBoxNetAddress.Text = toHex(block.Address);
            if (block.SpeedOpros == 0)
                numericUpDownSpeed.Value = numericUpDownSpeed.Maximum;
            else
                numericUpDownSpeed.Value = (decimal)(block.SpeedOpros * 0.0052);//block.SpeedOpros = 1;

            if (block.TypeCRC == 0xcc)
            {
                comboBoxTypeCRC.SelectedIndex = 0;
            }
            else
                comboBoxTypeCRC.SelectedIndex = 1;

            if (block.Speed == 0x05)
            {
                comboBoxSpeed.SelectedIndex = 1;
            }
            else
                comboBoxSpeed.SelectedIndex = 0;

            int number = 1;
            foreach (CmdOpros cmd in block.Cmds)
            {
                ListViewItem item = new ListViewItem(number.ToString());

                ListViewItem.ListViewSubItem netAddressOfOpros = new ListViewItem.ListViewSubItem(item,
                    string.Format("{0:X2}", cmd.Address));

                ListViewItem.ListViewSubItem dataLenght = new ListViewItem.ListViewSubItem(item,
                    string.Format("{0:X2}", cmd.SizeBuffer));

                item.SubItems.Add(netAddressOfOpros);
                item.SubItems.Add(dataLenght);

                listViewCmds.Items.Add(item);
                number = number + 1;
            }

            numericUpDownPerecl1.Value = block.Perecl1;
            numericUpDownPerecl2.Value = block.Perecl2;
        }

        string toHex(byte number)
        {
            return string.Format("{0:X2}", number);
        }

        private void listViewCmds_DoubleClick(object sender, EventArgs e)
        {
            EditOprosCmdForm frm = new EditOprosCmdForm();

            int index = listViewCmds.SelectedIndices[0];

            frm.comboBoxAddress.Text = string.Format("{0:X2}", block.Cmds[index].Address);
            frm.comboBoxSize.Text = string.Format("{0:X2}", block.Cmds[index].SizeBuffer); ;

            ListViewItem item = listViewCmds.SelectedItems[0];

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                item.SubItems[1].Text = frm.comboBoxAddress.Text;
                item.SubItems[2].Text = frm.comboBoxSize.Text;

                block.Cmds[index].Address = byte.Parse(frm.comboBoxAddress.Text, NumberStyles.AllowHexSpecifier);
                block.Cmds[index].SizeBuffer = byte.Parse(frm.comboBoxSize.Text, NumberStyles.AllowHexSpecifier);
            }
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            block.Address = byte.Parse(comboBoxNetAddress.Text, NumberStyles.AllowHexSpecifier);

            switch (comboBoxSpeed.Text)
            {
                case "38400":
                    
                    block.Speed = 0x00; 
                    break;

                case "57600":
                    
                    block.Speed = 0x05;
                    break;

                default:

                    block.Speed = 0x00;
                    break;
            }

            switch (comboBoxTypeCRC.Text)
            {
                case "CRC16(два байта)":

                    block.TypeCRC = 0xCC; 
                    break;

                case "Однобайтовая по модулю 255": 
                    
                    block.TypeCRC = 0x00; 
                    break;

                default:

                    block.TypeCRC = 0x00; 
                    break;
            }

            block.SpeedOpros = (byte)((double)numericUpDownSpeed.Value / 0.0052);

            byte num = (byte)this.numericUpDownPerecl1.Value;
            
            num = (byte)(num << 4);
            num = (byte)(num | ((byte)this.numericUpDownPerecl2.Value));
            
            block.NumbersOfIndicators = num;
        }
    }
}