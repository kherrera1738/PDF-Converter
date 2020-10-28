using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Document = iTextSharp.text.Document;

namespace PDF_converter
{
    public partial class SelectForm : Form
    {

        Panel opsPanel;
        string path;
        int ratio;

        public SelectForm()
        {
            InitializeComponent();
            ToOps.Enabled = false;
        }

        // Event Handlers

        private void ToOps_Click(object sender, EventArgs e)
        {
            List<string> files = GetFiles(LocationTB.Text);
            path = LocationTB.Text;
            Int32.TryParse(SizeTB.Text, out ratio);

            if (files.Count > 0)
            {
                // Create new Panel
                opsPanel = CreateOpsPanel();
                this.Controls.Add(opsPanel);

                // Add files found to new list box
                Control[] controls = opsPanel.Controls.Find("filesFoundList", false);
                ListBox filesFoundList = (ListBox)controls[0];

                filesFoundList.BeginUpdate();

                foreach (string file in files)
                {
                    filesFoundList.Items.Add(file.Remove(0, LocationTB.Text.Length + 1));
                }

                filesFoundList.EndUpdate();

                // Hide previous panel and add new panel
                selectPanel.Hide();
                opsPanel.Show();
            }
            else 
            {
                MessageBox.Show("Count not find images in folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
            // Get foundList and item to move to ignore list
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
            // Get ignore list and item to more to found list
            Control[] controls = opsPanel.Controls.Find("filesToIgnoreList", false);
            ListBox filesToIgnoreList = (ListBox)controls[0];
            Object selected = filesToIgnoreList.SelectedItem;

            if (selected != null) 
            {
                // Get found list to add selected item
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

        private void StartConversion(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.path + "\\downsized"))
            {
                System.Diagnostics.Debug.WriteLine("directory does not exist");
                Directory.CreateDirectory(this.path + "\\downsized");
                System.Diagnostics.Debug.WriteLine("directory was created");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Direcotry exists");
            }

            Control[] controls = opsPanel.Controls.Find("filesFoundList", false);
            ListBox filesFoundList = (ListBox)controls[0];
            foreach (string image_name in filesFoundList.Items)
            {
                System.Diagnostics.Debug.WriteLine(this.path + "\\" + image_name);
                System.Drawing.Image image = System.Drawing.Image.FromFile(this.path + "\\" + image_name);
                System.Diagnostics.Debug.WriteLine((int)(image.Width * this.ratio / 100.0) + " " + (int)(image.Height * this.ratio / 100.0));
                Bitmap resized = ResizeImage(image, (int)(image.Width * this.ratio / 100.0), (int)(image.Height * this.ratio / 100.0));
                System.Diagnostics.Debug.WriteLine("Resized");
                resized.Save(this.path + "\\downsized\\" + image_name, System.Drawing.Imaging.ImageFormat.Jpeg);
                System.Diagnostics.Debug.WriteLine("Saved " + this.path + "\\downsized\\" + image_name);
            }

            System.Windows.Forms.Application.Exit();
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

            /*            while (directories.Count > 0)
                        {
                            currentDirectory = directories.Dequeue();
                            files.AddRange(Directory.EnumerateFiles(currentDirectory, "*.jpg"));
                            files.AddRange(Directory.EnumerateFiles(currentDirectory, "*.jpeg"));
                            foreach (string dir in Directory.EnumerateDirectories(currentDirectory))
                            {
                                directories.Enqueue(dir);
                            }

                        }*/
            files.AddRange(Directory.EnumerateFiles(currentDirectory, "*.jpg"));
            files.AddRange(Directory.EnumerateFiles(currentDirectory, "*.jpeg"));
            return files;
        }

        // Change size of image
        private Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        // Saves images to pdf
        private void SaveToPDF(string[] imageNames, string path, string pdfName)
        {
            Document document = new Document();
            foreach(string name in imageNames)
            {
                using (var stream = new FileStream(path + pdfName + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();
                    using (var imageStream = new FileStream(path + name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var image = iTextSharp.text.Image.GetInstance(imageStream);
                        document.Add(image);
                    }
                    document.Close();
                }
            }
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
                Location = new Point(10, 30),
                Size = new Size(100, 170),
                Name = "filesFoundList"
            };

            Label FilesFoundLbl = new Label
            {
                Location = new Point(25, 10),
                Name = "FilesFoundLbl",
                Text = "Files Found"
            };

            ListBox filesToIgnoreList = new ListBox
            {
                Location = new Point(325, 30),
                Size = new Size(100, 170),
                Name = "filesToIgnoreList"
            };

            Label FilesToIgnoreLbl = new Label
            {
                Location = new Point(335, 10),
                Name = "FilesToIgnoreLbl",
                Text = "Files to Ignore"
            };

            Button FoundToIgnoreBtn = new Button
            {
                Location = new Point(185, 130),
                Name = "FoundToIgnoreBtn",
                Text = char.ConvertFromUtf32(0x2192)
            };

            FoundToIgnoreBtn.Click += new System.EventHandler(FoundToIgnoreBtn_Click);

            Button IgnoreToFoundBTn = new Button
            {
                Location = new Point(185, 80),
                Name = "IgnoreToFoundBtn",
                Text = char.ConvertFromUtf32(0x2190)
            };

            IgnoreToFoundBTn.Click += new System.EventHandler(IgnoreToFoundBtn_Click);

            Button OkConvertBtn = new Button
            {
                Location = new Point(337, 220),
                Name = "OkConvertBtn",
                Text = "OK"
            };

            OkConvertBtn.Click += new System.EventHandler(StartConversion);

            System.Diagnostics.Debug.WriteLine("Created Panel");

            // Add controls to new Panel
            opsPanel.Controls.Add(filesFoundList);
            opsPanel.Controls.Add(FilesFoundLbl);
            opsPanel.Controls.Add(filesToIgnoreList);
            opsPanel.Controls.Add(FilesToIgnoreLbl);
            opsPanel.Controls.Add(FoundToIgnoreBtn);
            opsPanel.Controls.Add(IgnoreToFoundBTn);
            opsPanel.Controls.Add(OkConvertBtn);

            return opsPanel;
        }
    }
}
