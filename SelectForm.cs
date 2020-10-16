using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF_converter
{
    public partial class SelectForm : Form
    {

        Panel opsPanel;

        public SelectForm()
        {
            InitializeComponent();
            ToOps.Enabled = false;


        }

        // Event Handlers

        private void ToOps_Click(object sender, EventArgs e)
        {
            // Create new Panel

            this.Height += 100;
            
            Panel opsPanel = new Panel
            {
                Location = new Point(26, 12),
                Name = "opsPanel",
                Size = new Size(selectPanel.Size.Width, this.Height-30)
            };

            this.opsPanel = opsPanel;

            // Create Controls for new panel
            ListBox filesFoundList = new ListBox
            {
                Location = new Point(10, 50),
                Size = new Size(100, 200)
            };

            Label FilesFoundLbl = new Label
            {
                Location = new Point(25, 30),
                Text = "Files Found"
            };

            ListBox filesToIgnoreList = new ListBox
            {
                Location = new Point(325, 50),
                Size = new Size(100, 200)
            };

            Label FilesToIgnoreLbl = new Label
            {
                Location = new Point(335, 30),
                Text = "Files to Ignore"
            };

            Button FoundToIgnoreBtn = new Button
            {
                Location = new Point(185, 150),
                Text = char.ConvertFromUtf32(0x2192)
            };

            Button IgnoreToFoundBTn = new Button
            {
                Location = new Point(185, 100),
                Text = char.ConvertFromUtf32(0x2190)
            };

            System.Diagnostics.Debug.WriteLine("Created Panel");

            // Add controls to new Panel
            opsPanel.Controls.Add(filesFoundList);
            opsPanel.Controls.Add(FilesFoundLbl);
            opsPanel.Controls.Add(filesToIgnoreList);
            opsPanel.Controls.Add(FilesToIgnoreLbl);
            opsPanel.Controls.Add(FoundToIgnoreBtn);
            opsPanel.Controls.Add(IgnoreToFoundBTn);
            this.Controls.Add(opsPanel);

            // Add files found to new list box
            List<string> files = GetFiles(LocationTB.Text);

            filesFoundList.BeginUpdate();

            foreach(string file in files)
            {
                filesFoundList.Items.Add(file.Remove(0, LocationTB.Text.Length + 1));
            }

            filesFoundList.EndUpdate();

            // Hide previous panel and add new panel
            selectPanel.Hide();
            opsPanel.Show();
        }

        // Open files browser when button clicked

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                LocationTB.Text = fbd.SelectedPath;
            }
        }

        // Validate if resize and location filled in

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

        // Moves file name from convert list to ignore list
        private void FoundToIgnoreBtn_Click(object sender, EventArgs e)
        {
            
        }

        // Validation Functions

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

        // Utility Functions

        private List<string> GetFiles(string path)
        {
            string currentDirectory = path;
            Queue<string> directories = new Queue<string>();
            List<string> files = new List<string>();
            directories.Enqueue(currentDirectory);

            while (directories.Count > 0)
            {
                currentDirectory = directories.Dequeue();
                files.AddRange(Directory.EnumerateFiles(currentDirectory, "*.jpg"));
                files.AddRange(Directory.EnumerateFiles(currentDirectory, "*.jpeg"));
                foreach (string dir in Directory.EnumerateDirectories(currentDirectory))
                {
                    directories.Enqueue(dir);
                }

            }

            return files;
        }
    }
}
