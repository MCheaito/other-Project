using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZipUtility;

namespace cheaito.libaray.tools
{
    public partial class frmCompressing : Form
    {
        private string directoryPathOrig;
        private string directoryPathDest;

        public frmCompressing(string directoryPathOrig, string directoryPathDest)
        {
            InitializeComponent();
            this.directoryPathOrig = directoryPathOrig;
            this.directoryPathDest = directoryPathDest;
        }

        private void CompressFiles()
        {
            int prog = 0;
            this.progressBar1.Maximum = Directory.GetFiles(this.directoryPathOrig).Length;
            this.progressBar1.Minimum = 0;

            //clean up destination floder
            foreach (String origFile in Directory.GetFiles(directoryPathDest))
            {
                if ((origFile.ToLower().IndexOf(".zip") > 0))
                {
                    File.Delete(origFile);
                }
                Application.DoEvents();
            }
            // Compress the current directory
            foreach (String origFile in Directory.GetFiles(directoryPathOrig))
            {
                prog++;
                this.progressBar1.Value = prog;
                //Seulement les dll et les .resources
                if ((origFile.ToLower().IndexOf(".resources") > 0) || (origFile.ToLower().IndexOf(".dll") > 0))
                {
                    this.textBox3.Text = "Compressing:" + Path.GetFileName(origFile);
                    ZipUtility.ZipManager.CompressFile(origFile, this.directoryPathDest, (origFile.ToLower().IndexOf(".resources") > 0));
                    Application.DoEvents();
                }
            }
            MessageBox.Show("Done!");
            this.Close();
        }
    }
}