using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using ZipUtility;
using System.Reflection;

namespace TestZipUtility
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox deleteOldFiles;
        private Label label2;
        private TextBox textBox2;
        private CheckBox chkDontStripExtension;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteOldFiles = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.chkDontStripExtension = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Zip Now";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(264, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "C:\\Allies";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Folder :";
            // 
            // deleteOldFiles
            // 
            this.deleteOldFiles.Location = new System.Drawing.Point(8, 68);
            this.deleteOldFiles.Name = "deleteOldFiles";
            this.deleteOldFiles.Size = new System.Drawing.Size(100, 24);
            this.deleteOldFiles.TabIndex = 3;
            this.deleteOldFiles.Text = "Delete old files";
            this.deleteOldFiles.CheckedChanged += new System.EventHandler(this.deleteOldFiles_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Extension :";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(80, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(264, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = ".xml";
            // 
            // chkDontStripExtension
            // 
            this.chkDontStripExtension.Location = new System.Drawing.Point(114, 68);
            this.chkDontStripExtension.Name = "chkDontStripExtension";
            this.chkDontStripExtension.Size = new System.Drawing.Size(176, 24);
            this.chkDontStripExtension.TabIndex = 6;
            this.chkDontStripExtension.Text = "Dont strip extension";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(392, 144);
            this.Controls.Add(this.chkDontStripExtension);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.deleteOldFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Zip Files";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
            textBox1.Text = Directory.GetCurrentDirectory();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			button1.Enabled = false;
			CompressFiles(textBox1.Text, textBox2.Text);
			button1.Enabled = true;
		}


		public void CompressFiles(String directoryPath, String extToCompress)
		{
			String destFile;

			// Compress the current directory
			foreach (String origFile in Directory.GetFiles(directoryPath)) 
			{
                if (origFile.ToLower().IndexOf(extToCompress) > 0)
				{
                    if (chkDontStripExtension.Checked)
                    {
                        destFile = origFile + ".zip";
                    }
                    else
                    {
                        destFile = origFile.Substring(0, origFile.ToLower().IndexOf(extToCompress)) + ".zip";
                    }
					ZipUtility.ZipManager.CompressFile(origFile, destFile);

					if (deleteOldFiles.Checked)
						File.Delete(origFile);
				}
			}
		
			// Compress the sub directories
			foreach (String subDirectory in Directory.GetDirectories(directoryPath))
			{
                CompressFiles(subDirectory, extToCompress);
			}
			
		}

        private void deleteOldFiles_CheckedChanged(object sender, EventArgs e)
        {

        }
	}
}
