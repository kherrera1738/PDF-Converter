using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF_converter
{
    public partial class SelectForm : Form
    {

        public SelectForm()
        {
            InitializeComponent();
            ToOps.Enabled = false;
        }

        private void ToOps_Click(object sender, EventArgs e)
        {

            this.Height *= 2;
            
            Panel opsPanel = new Panel
            {
                Location = new Point(26, 12),
                Name = "opsPanel",
                Size = new Size(200, 30)
            };

            TextBox opsText = new TextBox
            {
                Location = new Point(10, 10),
                Text = "This is the next form",
                Size = new Size(200, 30)
            };

            System.Diagnostics.Debug.WriteLine("Created Panel");

            opsPanel.Controls.Add(opsText);
            this.Controls.Add(opsPanel);

            selectPanel.Hide();
            opsPanel.Show();
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                LocationTB.Text = fbd.SelectedPath;
            }
        }

        private void SizeTB_TextChanged(object sender, EventArgs e)
        {
            ToOps.Enabled = BrowseFormValidation();
        }

        private void SizeTB_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void LocationTB_TextChanged(object sender, EventArgs e)
        {
            ToOps.Enabled = BrowseFormValidation();
        }

        private bool BrowseFormValidation()
        {
            if (Int32.TryParse(SizeTB.Text, out int sizeTbValue) && LocationTB.Text != "")
            {
                if (sizeTbValue >= 1 && sizeTbValue <= 100)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
