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
            opsPanel = CreateOpsPanel();
            this.Controls.Add(opsPanel);

            // Add files found to new list box
            List<string> files = GetFiles(LocationTB.Text);
            Control[] controls = opsPanel.Controls.Find("filesFoundList", false);
            ListBox filesFoundList = (ListBox) controls[0];

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

        private void LocationTB_TextChanged(object sender, EventArgs e)
        {
            ToOps.Enabled = BrowseFormValidation();
        }

        // Moves file name from convert list to ignore list
        private void FoundToIgnoreBtn_Click(object sender, EventArgs e)
        {
            // Get foundList and item to more to ignore list
            Control[] controls = opsPanel.Controls.Find("filesFoundList", false);
            ListBox filesFoundList = (ListBox) controls[0];
            Object selected = filesFoundList.SelectedItem;

            if (selected != null)
            {
                // Get ignoreList to add selected item
                controls = opsPanel.Controls.Find("filesToIgnoreList", false);
                ListBox filesToIgnoreList = (ListBox)controls[0];

                filesToIgnoreList.BeginUpdate();
                filesToIgnoreList.Items.Add(selected);
                filesToIgnoreList.EndUpdate();

                filesFoundList.BeginUpdate();
                filesFoundList.Items.Remove(selected);
                filesFoundList.EndUpdate();
            }
        }

        private void IgnoreToFoundBtn_Click(object sender, EventArgs e) 
        {
            // Get foundList and item to more to ignore list
            Control[] controls = opsPanel.Controls.Find("filesToIgnoreList", false);
            ListBox filesToIgnoreList = (ListBox)controls[0];
            Object selected = filesToIgnoreList.SelectedItem;

            if (selected != null) 
            {
                // Get ignoreList to add selected item
                controls = opsPanel.Controls.Find("filesFoundList", false);
                ListBox filesFoundList = (ListBox)controls[0];

                filesFoundList.BeginUpdate();
                filesFoundList.Items.Add(selected.ToString());
                filesFoundList.EndUpdate();

                filesToIgnoreList.BeginUpdate();
                filesToIgnoreList.Items.Remove(selected.ToString());
                filesToIgnoreList.EndUpdate();
            }
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

        private Panel CreateOpsPanel()
        {
            this.Height += 100;

            Panel opsPanel = new Panel
            {
                Location = new Point(26, 12),
                Name = "opsPanel",
                Size = new Size(selectPanel.Size.Width, this.Height - 30)
            };

            // Create Controls for new panel
            ListBox filesFoundList = new ListBox
            {
                Location = new Point(10, 50),
                Size = new Size(100, 200),
                Name = "filesFoundList"
            };

            Label FilesFoundLbl = new Label
            {
                Location = new Point(25, 30),
                Name = "FilesFoundLbl",
                Text = "Files Found"
            };

            ListBox filesToIgnoreList = new ListBox
            {
                Location = new Point(325, 50),
                Size = new Size(100, 200),
                Name = "filesToIgnoreList"
            };

            Label FilesToIgnoreLbl = new Label
            {
                Location = new Point(335, 30),
                Name = "FilesToIgnoreLbl",
                Text = "Files to Ignore"
            };

            Button FoundToIgnoreBtn = new Button
            {
                Location = new Point(185, 150),
                Name = "FoundToIgnoreBtn",
                Text = char.ConvertFromUtf32(0x2192)
            };

            FoundToIgnoreBtn.Click += new System.EventHandler(FoundToIgnoreBtn_Click);

            Button IgnoreToFoundBTn = new Button
            {
                Location = new Point(185, 100),
                Name = "IgnoreToFoundBtn",
                Text = char.ConvertFromUtf32(0x2190)
            };

            IgnoreToFoundBTn.Click += new System.EventHandler(IgnoreToFoundBtn_Click);

            System.Diagnostics.Debug.WriteLine("Created Panel");

            // Add controls to new Panel
            opsPanel.Controls.Add(filesFoundList);
            opsPanel.Controls.Add(FilesFoundLbl);
            opsPanel.Controls.Add(filesToIgnoreList);
            opsPanel.Controls.Add(FilesToIgnoreLbl);
            opsPanel.Controls.Add(FoundToIgnoreBtn);
            opsPanel.Controls.Add(IgnoreToFoundBTn);

            return opsPanel;
        }
    }
}
