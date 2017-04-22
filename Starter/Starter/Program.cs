using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Starter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Main());
            Main app = new Main();
            Application.Run();
        }

        static void icn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
    }
}