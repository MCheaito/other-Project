using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZipUtility;

namespace PackagingDllRes
{
    public partial class frmCompressing : Form
    {
        private string directoryPathOrigDll;
        private string directoryPathDestDll;
        private string directoryPathOrigRes;
        private string directoryPathDestRes;

        public frmCompressing(string directoryPathOrigDll, string directoryPathDestDll, string directoryPathOrigRes, string directoryPathDestRes)
        {
            InitializeComponent();
            this.directoryPathOrigDll = directoryPathOrigDll;
            this.directoryPathDestDll = directoryPathDestDll;
            this.directoryPathOrigRes = directoryPathOrigRes;
            this.directoryPathDestRes = directoryPathDestRes;
        }

        private void CompressFiles(string directoryPathOrig,string directoryPathDest)
        {
            int prog = 0;
            this.progressBar1.Maximum = Directory.GetFiles(directoryPathOrig).Length;
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
                    ZipUtility.ZipManager.CompressFile(origFile, directoryPathDest, (origFile.ToLower().IndexOf(".resources") > 0));
                    Application.DoEvents();
                }
            }
            this.Close();
        }
    }
}