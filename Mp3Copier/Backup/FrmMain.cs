using System;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Mp3Copier
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtSrc;
		private System.Windows.Forms.Label lblSrc;
		private System.Windows.Forms.Button btSrc;
		private System.Windows.Forms.Label lblDest;
		private System.Windows.Forms.TextBox txtDest;
		private System.Windows.Forms.Button btDest;
		private System.Windows.Forms.ProgressBar Pbar;
		private System.Windows.Forms.Button btStart;
		private System.Windows.Forms.FolderBrowserDialog dlgFolder;
		private System.Windows.Forms.Label lblFilename;
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
			this.txtSrc = new System.Windows.Forms.TextBox();
			this.lblSrc = new System.Windows.Forms.Label();
			this.btSrc = new System.Windows.Forms.Button();
			this.lblDest = new System.Windows.Forms.Label();
			this.txtDest = new System.Windows.Forms.TextBox();
			this.btDest = new System.Windows.Forms.Button();
			this.Pbar = new System.Windows.Forms.ProgressBar();
			this.btStart = new System.Windows.Forms.Button();
			this.dlgFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.lblFilename = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtSrc
			// 
			this.txtSrc.Location = new System.Drawing.Point(106, 24);
			this.txtSrc.Name = "txtSrc";
			this.txtSrc.Size = new System.Drawing.Size(232, 20);
			this.txtSrc.TabIndex = 0;
			this.txtSrc.Text = "";
			// 
			// lblSrc
			// 
			this.lblSrc.BackColor = System.Drawing.Color.Transparent;
			this.lblSrc.Location = new System.Drawing.Point(2, 24);
			this.lblSrc.Name = "lblSrc";
			this.lblSrc.Size = new System.Drawing.Size(100, 16);
			this.lblSrc.TabIndex = 1;
			this.lblSrc.Text = "Source";
			// 
			// btSrc
			// 
			this.btSrc.Location = new System.Drawing.Point(343, 21);
			this.btSrc.Name = "btSrc";
			this.btSrc.Size = new System.Drawing.Size(22, 19);
			this.btSrc.TabIndex = 2;
			this.btSrc.Text = "...";
			this.btSrc.Click += new System.EventHandler(this.btSrc_Click);
			// 
			// lblDest
			// 
			this.lblDest.BackColor = System.Drawing.Color.Transparent;
			this.lblDest.Location = new System.Drawing.Point(2, 48);
			this.lblDest.Name = "lblDest";
			this.lblDest.Size = new System.Drawing.Size(100, 16);
			this.lblDest.TabIndex = 4;
			this.lblDest.Text = "Destination";
			// 
			// txtDest
			// 
			this.txtDest.Location = new System.Drawing.Point(106, 48);
			this.txtDest.Name = "txtDest";
			this.txtDest.Size = new System.Drawing.Size(232, 20);
			this.txtDest.TabIndex = 3;
			this.txtDest.Text = "";
			// 
			// btDest
			// 
			this.btDest.Location = new System.Drawing.Point(343, 53);
			this.btDest.Name = "btDest";
			this.btDest.Size = new System.Drawing.Size(22, 16);
			this.btDest.TabIndex = 5;
			this.btDest.Text = "...";
			this.btDest.Click += new System.EventHandler(this.btDest_Click);
			// 
			// Pbar
			// 
			this.Pbar.Location = new System.Drawing.Point(16, 120);
			this.Pbar.Name = "Pbar";
			this.Pbar.Size = new System.Drawing.Size(328, 23);
			this.Pbar.TabIndex = 6;
			// 
			// btStart
			// 
			this.btStart.Location = new System.Drawing.Point(152, 80);
			this.btStart.Name = "btStart";
			this.btStart.TabIndex = 7;
			this.btStart.Text = "&Start";
			this.btStart.Click += new System.EventHandler(this.btStart_Click);
			// 
			// lblFilename
			// 
			this.lblFilename.BackColor = System.Drawing.Color.Transparent;
			this.lblFilename.Location = new System.Drawing.Point(16, 152);
			this.lblFilename.Name = "lblFilename";
			this.lblFilename.Size = new System.Drawing.Size(328, 16);
			this.lblFilename.TabIndex = 8;
			this.lblFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(384, 182);
			this.Controls.Add(this.lblFilename);
			this.Controls.Add(this.btStart);
			this.Controls.Add(this.Pbar);
			this.Controls.Add(this.btDest);
			this.Controls.Add(this.lblDest);
			this.Controls.Add(this.txtDest);
			this.Controls.Add(this.btSrc);
			this.Controls.Add(this.lblSrc);
			this.Controls.Add(this.txtSrc);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

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

		private void btSrc_Click(object sender, System.EventArgs e)
		{
			dlgFolder.ShowNewFolderButton = false;
			if (dlgFolder.ShowDialog() == DialogResult.OK)
				txtSrc.Text = dlgFolder.SelectedPath;
		}

		private void btDest_Click(object sender, System.EventArgs e)
		{	
			dlgFolder.ShowNewFolderButton = true;
			if (dlgFolder.ShowDialog() == DialogResult.OK)
				txtDest.Text = dlgFolder.SelectedPath;		
		}

		private void btStart_Click(object sender, System.EventArgs e)
		{

			if (txtSrc.Text == "")
			{
				MessageBox.Show("Please, Enter source path");
				txtSrc.Focus();
				return;
			}
			if (txtDest.Text == "")
			{
				MessageBox.Show("Please, Enter destination path");
				txtDest.Focus();
				return;
			}
		
			

			string [] mp3Filename = Directory.GetFiles(txtSrc.Text,"*.mp3");
			string [] wmaFilename = Directory.GetFiles(txtSrc.Text,"*.wma");
			string [] filesname = new  string [mp3Filename.Length + wmaFilename.Length];

			Array.Copy(mp3Filename,filesname,mp3Filename.Length);
			Array.Copy(wmaFilename,0,filesname,mp3Filename.Length,wmaFilename.Length);

		
		
			mp3info.mp3info mi = new mp3info.mp3info();
			Pbar.Minimum = 0;
			Pbar.Maximum = filesname.Length ;

			foreach (string fl in filesname)
 			{
				string artist="";
				string album="";
			
				lblFilename.Text = fl.Substring(fl.LastIndexOf("\\")+ 1);;
				string DirDest = txtDest.Text; 
				mi.filename = fl;
				mi.ReadID3v1();


				if (mi.hasID3v1)
				{
					artist = getLegaleFilename(mi.id3v1.Artist);
					album = getLegaleFilename(mi.id3v1.Album);

				}
				else
				{
					mi.ReadID3v2();
					if (mi.hasID3v2)
					{
						artist = getLegaleFilename(mi.id3v2.Artist);
						album = getLegaleFilename(mi.id3v2.Album);
					}
				}

				if ( artist !="")
					DirDest += "\\" + artist;
				if ( album !="")
					DirDest += "\\" + album;

				string fileDest = DirDest + "\\"+ lblFilename.Text;
				try
				{
					if (fl != fileDest)
					{
						if (!Directory.Exists(DirDest))
						{
							Directory.CreateDirectory(DirDest);
						}


						File.Move(fl,fileDest);
					}
				}
				catch 
				{
					File.Move(fl,this.txtDest.Text + "\\___"+lblFilename.Text );
				}
				finally
				{
					Application.DoEvents();
					Pbar.Increment(1);
				}
			

			}
			MessageBox.Show(this,"Completed!","Sorting files");
			Pbar.Value = 0;
			lblFilename.Text = "";
		}


		private string getLegaleFilename(string str)
		{
			//	fileDest	"D:\\Music\\chanson\\Yelo Molo\\Mili-MoloS[\fu\\Yelo Molo - Re-Feel.mp3"	string
			string f = str; 
			f =f.Replace("\0","");
			f =f.Replace("/","-");
			f =f.Replace(":","-");
			f =f.Replace("*","-");
			f =f.Replace("?","-");
			f =f.Replace("\"","-");
			f =f.Replace(">",")");
			f =f.Replace("<","(");
			f =f.Replace("|","_");
			f =f.Replace("","");
			f =f.Replace("[","");
			f =f.Replace("","");
			f =f.Replace("","");
			f =f.Replace("\\","");
			return f;
		}
	}
}
