using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace FusionODCPreRequisetesAnalyser.Applications
{
    class startSetup
    {
        public static void callInstallation(string setup, bool c, string commandLineArgs )
        {
           
            //Create a new Process object
            Process p = new Process();

            //Set the file that will be executed
            p.StartInfo.FileName = setup;

            //Set the parameters to start the process with
            p.StartInfo.Arguments = commandLineArgs;

            //Hide the window (if applicable), or display normally
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.UseShellExecute = c;
            p.StartInfo.CreateNoWindow = true;
            //Start the process
            p.Start();
            
            //Block this thread until the process completes
            p.WaitForExit();
            
        }
    }
}