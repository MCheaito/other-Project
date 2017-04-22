using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;


namespace cheaito.libaray.tools
{
    class Deployment
    {
        const string SystemXmlPathname = "Server\\Dnd.Cda.Allies.Server.AlliesWebSite\\App_Data\\Config\\System\\System.Xml";
        const string AlliesPath = "D:\\ALLIESWEB\\CURRENT\\Source\\";
        const string ReleaseRessourcePath = "D:\\ALLIESWEB\\CURRENT\\Binaries\\Resources";
        const string ReleaseAssembliesPath = "D:\\ALLIESWEB\\CURRENT\\Binaries\\Release";
        const string DebugRessourcePath = "D:\\ALLIESWEB\\CURRENT\\Binaries\\Resources";
        const string DebugAssembliesPath = "D:\\ALLIESWEB\\CURRENT\\Binaries\\Debug";

        public void SetLocalReleaseParamter()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AlliesPath + SystemXmlPathname);
            XmlNode  nd  =  doc.SelectSingleNode("/Config/[ConfigName= \"LocalCmptAssembliesPath1\"]");
            MessageBox.Show(nd.OuterXml);

        }
    }
}
