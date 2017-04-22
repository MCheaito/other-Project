using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.IO;
using Starter.collections;
using System.Runtime.InteropServices;

namespace Starter
{
    class Main
    {
        private NotifyIcon icn;
        private ContextMenuStrip contextMainMenu;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2; 
        private ToolStripMenuItem actionsToolStripMenuItem;
        //private ComponentResourceManager resources;
        private TaskCollections mainList;

        //[DllImport("user32.dll")]
        //private static extern void mouse_event(
        //UInt32 dwFlags, // motion and click options
        //UInt32 dx, // horizontal position or change
        //UInt32 dy, // vertical position or change
        //UInt32 dwData, // wheel movement
        //IntPtr dwExtraInfo // application-defined information
        //);
        //const UInt32 MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        //const UInt32 MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */


        public Main()
        {

            icn   = new NotifyIcon();
            contextMainMenu = new ContextMenuStrip();

            this.toolStripMenuItem1 = new ToolStripMenuItem();
            this.toolStripMenuItem2 = new ToolStripMenuItem();
            this.actionsToolStripMenuItem = new ToolStripMenuItem();
            //this.resources = new ComponentResourceManager(typeof(Starter));
            //this.icn.Icon = ((System.Drawing.Icon)(resources.GetObject("worker.ico")));
           // this.icn.Icon = new System.Drawing.Icon(".\\images\\worker.ico");
            this.icn.Icon = new System.Drawing.Icon(Main.GetFileRessouces("Icons.worker.ico"));

            this.icn.ContextMenuStrip = this.contextMainMenu;
            //this.icn.MouseDown += new MouseEventHandler(icn_MouseDown);
            icn.Visible = true;

            // 
            // contextMainMenu
            // 
            this.contextMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.actionsToolStripMenuItem,
            this.toolStripMenuItem2});
            this.contextMainMenu.Name = "contextMainMenu";
            this.contextMainMenu.Size = new System.Drawing.Size(153, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Restore";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "Quit Application";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.actionsToolStripMenuItem.Text = "Actions";

            //
            // mainlist
            //
            this.mainList = new TaskCollections();

            CreateMenu();


        }

        //void icn_MouseDown(object sender, MouseEventArgs e)
        //{


        //    UInt32 x = (UInt32)e.X;
        //    UInt32 y = (UInt32)e.Y;

        //    mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, new IntPtr());
        //    mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, new IntPtr());


        //}
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            icn.Visible = false;
            Application.Exit();
        }

        private void CreateMenu()
        {

            XmlDocument xmlDoc = new XmlDocument();
            Stream inFile = Main.GetFileRessouces("mnuActions.xml");
            xmlDoc.Load(inFile);
            ToolStripMenuItem cm = this.actionsToolStripMenuItem;

            // Select and display all book titles.
            XmlNodeList nodeList;
            XmlElement root = xmlDoc.DocumentElement;
            nodeList = root.SelectNodes("/items/item");
            foreach (XmlNode item in nodeList)
            {
                ToolStripMenuItem cmi = new ToolStripMenuItem();
                Task tsk = mainList.Create(cmi);
                tsk.Id = Guid.NewGuid().ToString();
                tsk.Type = item.Attributes["type"].Value;
                tsk.Name = item.Attributes["name"].Value;
                tsk.Repository = item.Attributes["feature"].Value; //Dll name
                tsk.Feature = item.Attributes["id"].Value; //nameSpace . class . method
                tsk.Parameters = item.Attributes["parameters"].Value;
                tsk.Startup = (item.Attributes["startup"].Value == "1");
                tsk.Icon = item.Attributes["image"].Value;

                mainList.Add(tsk);
                cmi.Text = tsk.Name;
                cmi.Tag = tsk.Id;
                cmi.Click += OnCmiClick;
                cm.DropDownItems.Add(cmi);

            }

        }
        // This method is going to be called every time user clicks any menu item  
        private void OnCmiClick(object sender, EventArgs e)
        {
            ToolStripMenuItem cmi = (ToolStripMenuItem)sender;
            mainList.Toggle(cmi.Tag.ToString());
        }

        public static Stream GetFileRessouces(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("Starter."+ filename);
        }
    }
}
