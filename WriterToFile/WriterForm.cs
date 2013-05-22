using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WriterToFile
{
    public partial class WriterForm : Form
    {
        public WriterForm()
        {
            InitializeComponent();
        }

        private void buttonBrowseFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxFilePath.Text = saveFileDialog.FileName;
            }
        }

        private DialogResult result = DialogResult.Cancel;


        public DialogResult Result
        {
            get { return result; }
        }

        public Filter[] Filters
        {
            get
            {
                List<Filter> fil = new List<Filter>();
                foreach (ListViewItem item in listView1.Items)
                {
                    Filter filter = new Filter();

                    filter.state = item.Checked;
                    filter.filter = item.SubItems[1].Text;

                    fil.Add(filter);
                }
                return fil.ToArray();
            }

            set
            {
                foreach (var item in value)
                {
                    ListViewItem it = new ListViewItem();
                    it.Checked = item.state;

                    ListViewItem.ListViewSubItem ladd = new ListViewItem.ListViewSubItem(it, item.filter);

                    it.SubItems.Add(ladd);
                    listView1.Items.Add(it);
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (IsValidLadd())
                {
                    ListViewItem item = new ListViewItem();
                    ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem(item, textBoxFilterValue.Text);
                    item.SubItems.Add(sub);
                    listView1.Items.Add(item);
                }
                else
                    MessageBox.Show(this, "Не корректно значение линейного адреса", "Информация", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        bool IsValidLadd()
        {
            try
            {
                if (textBoxFilterValue.Text.Length != 2)
                {
                    return false;
                }

                int hex = int.Parse(textBoxFilterValue.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
                return true;                    
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var selected = listView1.SelectedItems;
                foreach (ListViewItem item in selected)
                {
                    listView1.Items.Remove(item);
                }
            }
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}