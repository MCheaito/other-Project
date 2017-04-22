using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PackagingDllRes
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string GetPathname(string path)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = path;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath; 
            }
            return "";
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = this.GetPathname(txtSrcDll.Text);
            if (path != "") this.txtSrcDll.Text = path;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path = this.GetPathname(txtSrcRes.Text);
            if (path != "") this.txtSrcRes.Text = path;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string path = this.GetPathname(txtTrgDll.Text);
            if (path != "") this.txtTrgDll.Text = path;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string path = this.GetPathname(txtTrgRes.Text);
            if (path != "") this.txtTrgRes.Text = path;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            frmCompressing frm = new frmCompressing(this.txtSrcDll.Text,this.txtTrgDll.Text,this.txtSrcRes.Text,this.txtTrgRes.Text);
            frm.Show();
        }
    }
}