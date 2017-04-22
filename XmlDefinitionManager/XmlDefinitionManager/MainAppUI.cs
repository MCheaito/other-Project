using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XmlDefinitionManager
{
    public partial class MainAppUI : Form
    {
        private XmlDocument fDef;
        private TreeNode root;
        
        public MainAppUI()
        {
            root = new TreeNode();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofdlg.Filter = "Config file (*.xml)|";
            ofdlg.FileName = "*.xml";
            if (ofdlg.ShowDialog() != DialogResult.OK)
            {
            }
            fDef = new XmlDocument();
            fDef.Load(ofdlg.FileName);
            LoadTypes();
            root.ExpandAll();

           
        }
        private void LoadTypes()
        {
            
            tvConfig.Nodes.Clear(); // clear
            root = tvConfig.Nodes.Add("Configuration: ["+ fDef.BaseURI.Substring(8)+"]");
            foreach (XmlNode nd in fDef.SelectNodes("//types/type"))
            {
                TreeNode curnode = root.Nodes.Add(nd.Attributes.GetNamedItem("name").Value);
                LoadAttributes(curnode,nd);
                LoadContainers(curnode, nd);
            }
        }
        private void LoadAttributes(TreeNode treend, XmlNode xmlnd)
        {
            TreeNode attribNode = new TreeNode("Attributes");
            string name = xmlnd.Attributes.GetNamedItem("name").Value;

            treend.Nodes.Add(attribNode);
            foreach (XmlNode nd in xmlnd.SelectNodes("attributes/attribute"))
            {
                TreeNode curnode = attribNode.Nodes.Add(nd.Attributes.GetNamedItem("name").Value);
            }

        }
        private void LoadContainers(TreeNode treend, XmlNode xmlnd)
        {
            TreeNode attribNode = new TreeNode("Containers");
            XmlNodeList list = xmlnd.SelectNodes("containers/container");
            if (list.Count == 0) return;

            treend.Nodes.Add(attribNode);
            foreach (XmlNode nd in list)
            {
                TreeNode curnode = attribNode.Nodes.Add("<<"+nd.Attributes.GetNamedItem("name").Value+">>");
            }

        }


    }
}