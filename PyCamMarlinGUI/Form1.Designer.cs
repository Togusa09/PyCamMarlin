namespace PyCamMarlinGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OutputFile = new System.Windows.Forms.TextBox();
            this.OpenFile = new System.Windows.Forms.Button();
            this.ProcessFile = new System.Windows.Forms.Button();
            this.ImportFiles = new System.Windows.Forms.ListBox();
            this.RemoveFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OutputFile
            // 
            this.OutputFile.Location = new System.Drawing.Point(12, 209);
            this.OutputFile.Name = "OutputFile";
            this.OutputFile.Size = new System.Drawing.Size(209, 20);
            this.OutputFile.TabIndex = 1;
            // 
            // OpenFile
            // 
            this.OpenFile.Location = new System.Drawing.Point(239, 24);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(75, 23);
            this.OpenFile.TabIndex = 2;
            this.OpenFile.Text = "Add File";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // ProcessFile
            // 
            this.ProcessFile.Location = new System.Drawing.Point(239, 115);
            this.ProcessFile.Name = "ProcessFile";
            this.ProcessFile.Size = new System.Drawing.Size(75, 23);
            this.ProcessFile.TabIndex = 2;
            this.ProcessFile.Text = "Process";
            this.ProcessFile.UseVisualStyleBackColor = true;
            this.ProcessFile.Click += new System.EventHandler(this.ProcessFile_Click);
            // 
            // ImportFiles
            // 
            this.ImportFiles.FormattingEnabled = true;
            this.ImportFiles.Location = new System.Drawing.Point(13, 13);
            this.ImportFiles.Name = "ImportFiles";
            this.ImportFiles.Size = new System.Drawing.Size(208, 186);
            this.ImportFiles.TabIndex = 3;
            // 
            // RemoveFile
            // 
            this.RemoveFile.Location = new System.Drawing.Point(239, 54);
            this.RemoveFile.Name = "RemoveFile";
            this.RemoveFile.Size = new System.Drawing.Size(75, 23);
            this.RemoveFile.TabIndex = 4;
            this.RemoveFile.Text = "Remove";
            this.RemoveFile.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 255);
            this.Controls.Add(this.RemoveFile);
            this.Controls.Add(this.ImportFiles);
            this.Controls.Add(this.ProcessFile);
            this.Controls.Add(this.OpenFile);
            this.Controls.Add(this.OutputFile);
            this.Name = "Form1";
            this.Text = "GCode Process";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox OutputFile;
        private System.Windows.Forms.Button OpenFile;
        private System.Windows.Forms.Button ProcessFile;
        private System.Windows.Forms.ListBox ImportFiles;
        private System.Windows.Forms.Button RemoveFile;
    }
}

