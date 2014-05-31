using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Configuration;
using log4net;
using FusionODCPreRequisetesAnalyser.Utils;
using System.Reflection;

namespace FusionODCPreRequisetesAnalyser.Applications
{
    class CustomInstaller
    {
        private static String communicatorProcessName = ConfigurationManager.AppSettings["Communicator-Process-Name"];
        private static String outlookProcessName = ConfigurationManager.AppSettings["Outlook-Process-Name"];
        private static String unifyAppdataFolder = ConfigurationManager.AppSettings["Unify-Appdata-Folder"];
        private static String filePathSuperPackage = ConfigurationManager.AppSettings["File-Path-Unify-SuperPackage"];
        private static readonly ILog logger;

        static CustomInstaller()
        {
            logger = Log4NetHelper.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info("Initializing CustomInstaller class...");
        }

        public static void InitiateInstallation()
        {
            logger.Info("Calling method InitiateInstallation()...");

            Process process = Process.Start(String.Format("{0}{1}", @filePathSuperPackage, "FusionDeployment.exe"));
            process.Refresh();
            process.WaitForExit();

            /*
            //Verifica se foi encerrado o primeiro setup
            if (process.HasExited)
            {
                process = Process.Start(@"C:\Users\gm000027\Downloads\wrar501.exe");
            }
            process.Refresh();
            process.WaitForExit();
            //Verifica se foi encerrado o segundo setup
            if (process.HasExited)
            {
                process = Process.Start(@"C:\Users\gm000027\Downloads\wrar501.exe");
            }
            */

            Console.WriteLine(String.Format("{0}{1}", @filePathSuperPackage, "FusionDeployment.exe"));
        }

        public static void CleanWorkDirectory()
        {
            logger.Info("Calling method CleanWorkDirectory()...");

            if (System.IO.Directory.Exists(@unifyAppdataFolder))
            {
                System.IO.Directory.Delete(@unifyAppdataFolder);
            }
        }

        public static void CloseNecessaryApplication()
        {
            logger.Info("Calling method CloseNecessaryApplication()...");

            if(Resources.isProcessRunning(communicatorProcessName))
                Resources.killProcess(communicatorProcessName);
            if (Resources.isProcessRunning(outlookProcessName))
                Resources.killProcess(outlookProcessName);
        }
    }
}