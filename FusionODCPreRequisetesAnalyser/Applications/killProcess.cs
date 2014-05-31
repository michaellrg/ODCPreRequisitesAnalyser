using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FusionODCPreRequisetesAnalyser.Applications
{
    class killProcess
    {
        public static void Kill(string name)
        {
            foreach (Process proc in Process.GetProcessesByName(name))
            {
                proc.Kill();
            }
        }
    }

}
