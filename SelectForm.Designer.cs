namespace PDF_converter
{
    partial class SelectForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectPanel = new System.Windows.Forms.Panel();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.LocationTB = new System.Windows.Forms.TextBox();
            this.SizeTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.loc_lbl = new System.Windows.Forms.Label();
            this.ToOps = new System.Windows.Forms.Button();
            this.selectPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectPanel
            // 
            this.selectPanel.Controls.Add(this.BrowseBtn);
            this.selectPanel.Controls.Add(this.LocationTB);
            this.selectPanel.Controls.Add(this.SizeTB);
            this.selectPanel.Controls.Add(this.label1);
            this.selectPanel.Controls.Add(this.loc_lbl);
            this.selectPanel.Controls.Add(this.ToOps);
            this.selectPanel.Location = new System.Drawing.Point(12, 12);
            this.selectPanel.Name = "selectPanel";
            this.selectPanel.Size = new System.Drawing.Size(467, 156);
            this.selectPanel.TabIndex = 0;
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Location = new System.Drawing.Point(369, 18);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.BrowseBtn.TabIndex = 5;
            this.BrowseBtn.Text = "Browse";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // LocationTB
            // 
            this.LocationTB.Location = new System.Drawing.Point(137, 18);
            this.LocationTB.Name = "LocationTB";
            this.LocationTB.Size = new System.Drawing.Size(212, 23);
            this.LocationTB.TabIndex = 4;
            this.LocationTB.TextChanged += new System.EventHandler(this.LocationTB_TextChanged);
            // 
            // SizeTB
            // 
            this.SizeTB.Location = new System.Drawing.Point(137, 58);
            this.SizeTB.Name = "SizeTB";
            this.SizeTB.Size = new System.Drawing.Size(40, 23);
            this.SizeTB.TabIndex = 3;
            this.SizeTB.TextChanged += new System.EventHandler(this.SizeTB_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(21, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resize Percent:";
            // 
            // loc_lbl
            // 
            this.loc_lbl.AutoSize = true;
            this.loc_lbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loc_lbl.Location = new System.Drawing.Point(21, 18);
            this.loc_lbl.Name = "loc_lbl";
            this.loc_lbl.Size = new System.Drawing.Size(64, 19);
            this.loc_lbl.TabIndex = 1;
            this.loc_lbl.Text = "Location:";
            // 
            // ToOps
            // 
            this.ToOps.Location = new System.Drawing.Point(369, 118);
            this.ToOps.Name = "ToOps";
            this.ToOps.Size = new System.Drawing.Size(75, 23);
            this.ToOps.TabIndex = 0;
            this.ToOps.Text = "OK";
            this.ToOps.UseVisualStyleBackColor = true;
            this.ToOps.Click += new System.EventHandler(this.ToOps_Click);
            // 
            // SelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 176);
            this.Controls.Add(this.selectPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SelectForm";
            this.Text = "Convert to PDF";
            this.selectPanel.ResumeLayout(false);
            this.selectPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ToOps;
        private System.Windows.Forms.Panel selectPanel;
        private System.Windows.Forms.TextBox LocationTB;
        private System.Windows.Forms.TextBox SizeTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label loc_lbl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BrowseBtn;
        private System.Windows.Forms.Button ToOpsBtn;
    }
}

