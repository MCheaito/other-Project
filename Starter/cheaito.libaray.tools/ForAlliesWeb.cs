using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

using EnvDTE;
using EnvDTE80;
using System.Runtime.InteropServices;


namespace cheaito.libaray.tools
{
    public class ForAlliesWeb
    {
        const string SystemXmlPathname = @"Server\Dnd.Cda.Allies.Server.AlliesWebSite\App_Data\Config\System\System.Xml";
        const string AlliesPath = @"D:\projets\ALLIESWEB\CURRENT\Source\";
        const string ReleaseRessourcePath = @"D:\projets\ALLIESWEB\CURRENT\Binaries\Resources";
        const string ReleaseAssembliesPath = @"D:\projets\ALLIESWEB\CURRENT\Binaries\Release";
        const string DebugRessourcePath = @"D:\projets\ALLIESWEB\CURRENT\Binaries\Resources";
        const string DebugAssembliesPath = @"D:\projets\ALLIESWEB\CURRENT\Binaries\Debug";
        const string filePath = @"D:\projets\ALLIESWEB\CURRENT\Source";
        const string fileName = "Allies Web.sln";

        XmlNode ndLocalCmptAssembliesPath;
        XmlNode ndLoadResourcesPath;
        XmlNode ndLoadLocalResources;
        XmlNode ndLoadLocalCmptAssemblies;
        XmlDocument doc;

        EnvDTE80.DTE2 dte;

        public string SetLocalDebugParamter()
        { 
             try
            {
                this.InitConfig();

                if ((ndLocalCmptAssembliesPath == null) || (ndLoadResourcesPath == null) || (ndLoadLocalResources == null) || (ndLoadLocalCmptAssemblies == null))
                    MessageBox.Show("Keys not found \nLocalCmptAssembliesPath\n,LoadResourcesPath\n,LoadLocalResources\n or\n LoadLocalCmptAssemblies\n ");
                else
                {
                    this.SetAlliesConfig(DebugAssembliesPath, DebugRessourcePath, "yes", "yes");
                }

                CompileSolution("Debug");

                //Properties prop = this.GetPublishProperties("Dnd.Cda.Allies.Client.ClickOnceLoader");
                //this.SetPublishProperty(prop, "PublishUrl", @"http://www.test.com");

                return "0";
            }
            catch (Exception e ) {
                MessageBox.Show(e.ToString());
                return "";
            }
       
        }
        public string SetLocalReleaseParamter()
        {
            try
            {
                this.InitConfig();

                if ((ndLocalCmptAssembliesPath == null) || (ndLoadResourcesPath == null) || (ndLoadLocalResources == null) || (ndLoadLocalCmptAssemblies == null))
                    MessageBox.Show("Keys not found \nLocalCmptAssembliesPath\n,LoadResourcesPath\n,LoadLocalResources\n or\n LoadLocalCmptAssemblies\n ");
                else
                {
                    this.SetAlliesConfig(ReleaseAssembliesPath, ReleaseRessourcePath, "yes", "yes");
                }

                CompileSolution("Release");

               // Properties prop = this.GetPublishProperties("Dnd.Cda.Allies.Client.ClickOnceLoader");
                //this.SetPublishProperty(prop, "PublishUrl", @"http://www.test.com");

                return "0";
            }
            catch (Exception e ) {
                MessageBox.Show(e.ToString());
                return "";
            }

        }

        public string PackagingAlliesWebClientFiles(string org, string dest)
        {
            frmCompressing frm = new frmCompressing(org, dest);
            frm.Show();

           
            return "";
        }

        private  void CompileSolution(string mode)
        {
            object obj = null;
            System.Type type = null;

            // Config Solution to build
            string fullName = Path.Combine(filePath, fileName);

            // Get the ProgID for DTE 8.0.
            type = System.Type.GetTypeFromProgID("VisualStudio.DTE.8.0", true);

            // Create a new instance of the IDE.
            obj = System.Activator.CreateInstance(type, true);
            // Cast the instance to DTE2 and assign to variable dte.
            dte = (EnvDTE80.DTE2)obj;


            // Register the IOleMessageFilter to handle any threading errors.
            MessageFilter.Register();
            // Display the Visual Studio IDE.
            dte.MainWindow.Activate();
            //Open the solution
            dte.Solution.Open(fullName);
            //Set Active configuration (debug or Release)
            dte.Solution.SolutionBuild.SolutionConfigurations.Item(mode).Activate();
            		
            //All projects should be build
            foreach (SolutionContext sc in dte.Solution.SolutionBuild.SolutionConfigurations.Item(mode).SolutionContexts)
            {
                sc.ShouldBuild = true;
            }
            dte.Solution.SolutionBuild.Build(true);

            // All done, so shut down the IDE...
            //dte.Quit();
            // and turn off the IOleMessageFilter.

            
               
            //MessageBox.Show("Closed!!!!");

            MessageFilter.Revoke();
        }

        /// <summary>
        /// set allies config files
        /// </summary>
        /// <param name="valLocalCmptAssembliesPath"></param>
        /// <param name="valLoadResourcesPath"></param>
        /// <param name="valLoadLocalResources"></param>
        /// <param name="valLoadLocalCmptAssemblies"></param>
        /// <returns></returns>
        private void SetAlliesConfig(string valLocalCmptAssembliesPath, string valLoadResourcesPath, string valLoadLocalResources, string valLoadLocalCmptAssemblies)
        {
            ndLocalCmptAssembliesPath.InnerText = valLocalCmptAssembliesPath;
            ndLoadResourcesPath.InnerText = valLoadResourcesPath;
            ndLoadLocalResources.InnerText = valLoadLocalResources;
            ndLoadLocalCmptAssemblies.InnerText = valLoadLocalCmptAssemblies;

            doc.Save(AlliesPath + SystemXmlPathname);

        }

        private void InitConfig()
        {
            doc = new XmlDocument();
            doc.Load(AlliesPath + SystemXmlPathname);

            //
            //Mettre à jour les clés    LocalCmptAssembliesPath,
            //                          LoadResourcesPath,
            //                          LoadLocalResources,
            //                          LoadLocalCmptAssemblies
            //
            ndLocalCmptAssembliesPath = doc.SelectSingleNode("//Config[ConfigName = \"LocalCmptAssembliesPath\"]/Value");
            ndLoadResourcesPath = doc.SelectSingleNode("//Config[ConfigName = \"LoadResourcesPath\"]/Value");
            ndLoadLocalResources = doc.SelectSingleNode("//Config[ConfigName = \"LoadLocalResources\"]/Value");
            ndLoadLocalCmptAssemblies = doc.SelectSingleNode("//Config[ConfigName = \"LoadLocalCmptAssemblies\"]/Value");
        }

        private EnvDTE.Properties GetPublishProperties(string projectName)
        {
            //Project proj = dte.Solution.Projects.Item(projectName);
            //Project proj = dte.Solution.Projects.Item("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC");
            ProjectItem proj = dte.Solution.FindProjectItem("Dnd.Cda.Allies.Client.ClickOnceLoader");
            EnvDTE.Property publishProperty = proj.Properties.Item("Publish");
            
            return ((EnvDTE.Properties)publishProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="publisherProperties"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        private void SetPublishProperty(EnvDTE.Properties publisherProperties, string propertyName, string value)
        {
            if (publisherProperties == null)
            {
                publisherProperties.Item(propertyName).Value = value;
            }
        }

        private string GetPublishProperty(EnvDTE.Properties publisherProperties, string propertyName)
        {
            if (publisherProperties == null)
            {
                return publisherProperties.Item(propertyName).Value.ToString();
            }
            return "";
        }


    }

    #region Class containing the IOleMessageFilter thread error-handling functions
    public class MessageFilter : IOleMessageFilter
    {
        //The start filter
        public static void Register()
        {
            IOleMessageFilter newFilter = new MessageFilter();
            IOleMessageFilter oldFilter = null;
            CoRegisterMessageFilter(newFilter, out oldFilter);
        }

        //Close filter when it's done
        public static void Revoke()
        {
            IOleMessageFilter oldFilter = null;
            CoRegisterMessageFilter(null, out oldFilter);
        }

        //IOleMessageFilter functions handle incomming thread request
        int IOleMessageFilter.HandleInCommingCall(int dwCallType, IntPtr hTaskCaller, int dwTickCount, IntPtr lpInterfaceInfo)
        {
            //Return flag SERVERCALL_ISHANDLED
            return 0;
        }

        //Thread call was rejected, so try again 
        int IOleMessageFilter.RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType)
        {
            if (dwRejectType == 2)
            {
                // Retry the thread Immediatley if return >= 0 & < 100
                return 99;
            }
            //Too busy 
            return -1;
        }

        int IOleMessageFilter.MessagePending(IntPtr hTaskCallee,
            int dwTickCount,
            int dwPendingTyp)
        {
            //Return the flag PENDINGMSG_WAITDEFPROSS
            return 2;
        }

        [DllImport("Ole32.dll")]
        private static extern int CoRegisterMessageFilter(IOleMessageFilter newFilter, out IOleMessageFilter oldFilter);

    } 
    #endregion

    #region Implement the interface IOleMessageFilter
    [ComImport(), Guid("00000016-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    interface IOleMessageFilter
    {
        [PreserveSig]
        int HandleInCommingCall(
            int dwCallType,
            IntPtr hTaskCaller,
            int dwTickCount,
            IntPtr lpInterfaceInfo);

        [PreserveSig]
        int RetryRejectedCall(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwRejectType);

        [PreserveSig]
        int MessagePending(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwPendingType);

    } 
    #endregion
}
