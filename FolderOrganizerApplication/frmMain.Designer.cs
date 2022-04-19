namespace FolderOrganizerApplication
{
    partial class frmMain
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
            this.fbdFolderSelect = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSelectSourceFolder = new System.Windows.Forms.Button();
            this.txtSourceFolderPath = new System.Windows.Forms.TextBox();
            this.txtDestinationFolderPath = new System.Windows.Forms.TextBox();
            this.btnSelectDestinationFolder = new System.Windows.Forms.Button();
            this.btnOrganize = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.prgOrganizing = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnSelectSourceFolder
            // 
            this.btnSelectSourceFolder.Location = new System.Drawing.Point(27, 32);
            this.btnSelectSourceFolder.Name = "btnSelectSourceFolder";
            this.btnSelectSourceFolder.Size = new System.Drawing.Size(94, 29);
            this.btnSelectSourceFolder.TabIndex = 0;
            this.btnSelectSourceFolder.Text = "...";
            this.btnSelectSourceFolder.UseVisualStyleBackColor = true;
            this.btnSelectSourceFolder.Click += new System.EventHandler(this.btnSelectSourceFolder_Click);
            // 
            // txtSourceFolderPath
            // 
            this.txtSourceFolderPath.Location = new System.Drawing.Point(153, 33);
            this.txtSourceFolderPath.Name = "txtSourceFolderPath";
            this.txtSourceFolderPath.ReadOnly = true;
            this.txtSourceFolderPath.Size = new System.Drawing.Size(246, 27);
            this.txtSourceFolderPath.TabIndex = 1;
            // 
            // txtDestinationFolderPath
            // 
            this.txtDestinationFolderPath.Location = new System.Drawing.Point(153, 103);
            this.txtDestinationFolderPath.Name = "txtDestinationFolderPath";
            this.txtDestinationFolderPath.ReadOnly = true;
            this.txtDestinationFolderPath.Size = new System.Drawing.Size(246, 27);
            this.txtDestinationFolderPath.TabIndex = 2;
            // 
            // btnSelectDestinationFolder
            // 
            this.btnSelectDestinationFolder.Location = new System.Drawing.Point(27, 101);
            this.btnSelectDestinationFolder.Name = "btnSelectDestinationFolder";
            this.btnSelectDestinationFolder.Size = new System.Drawing.Size(94, 29);
            this.btnSelectDestinationFolder.TabIndex = 3;
            this.btnSelectDestinationFolder.Text = "...";
            this.btnSelectDestinationFolder.UseVisualStyleBackColor = true;
            this.btnSelectDestinationFolder.Click += new System.EventHandler(this.btnSelectDestinationFolder_Click);
            // 
            // btnOrganize
            // 
            this.btnOrganize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOrganize.Location = new System.Drawing.Point(163, 161);
            this.btnOrganize.Name = "btnOrganize";
            this.btnOrganize.Size = new System.Drawing.Size(176, 29);
            this.btnOrganize.TabIndex = 4;
            this.btnOrganize.Text = "سازماندهی فایلها";
            this.btnOrganize.UseVisualStyleBackColor = true;
            this.btnOrganize.Click += new System.EventHandler(this.btnOrganize_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(414, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "پوشه مبدا";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(413, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "پوشه مقصد";
            // 
            // prgOrganizing
            // 
            this.prgOrganizing.Location = new System.Drawing.Point(27, 225);
            this.prgOrganizing.Name = "prgOrganizing";
            this.prgOrganizing.Size = new System.Drawing.Size(460, 29);
            this.prgOrganizing.TabIndex = 7;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 345);
            this.Controls.Add(this.prgOrganizing);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOrganize);
            this.Controls.Add(this.btnSelectDestinationFolder);
            this.Controls.Add(this.txtDestinationFolderPath);
            this.Controls.Add(this.txtSourceFolderPath);
            this.Controls.Add(this.btnSelectSourceFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.RightToLeftLayout = true;
            this.Text = "سازماندهی فولدرها";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FolderBrowserDialog fbdFolderSelect;
        private Button btnSelectSourceFolder;
        private TextBox txtSourceFolderPath;
        private TextBox txtDestinationFolderPath;
        private Button btnSelectDestinationFolder;
        private Button btnOrganize;
        private Label label1;
        private Label label2;
        private ProgressBar prgOrganizing;
    }
}