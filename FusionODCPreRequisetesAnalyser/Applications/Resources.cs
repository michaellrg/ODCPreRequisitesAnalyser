using FusionODCPreRequisetesAnalyser.Utils;
using log4net;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FusionODCPreRequisetesAnalyser.Applications
{
    public static class Resources
    {
        private static String registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        private static String registryKey64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
        public static String RegistryKey { get { return registryKey; } }
        public static String RegistryKey64 { get { return registryKey64; } }
        private static readonly ILog logger;

        static Resources()
        {
            logger = Log4NetHelper.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info("Initializing Resource class...");
        }

        public static String[] ListInstalledApplications()
        {
            String[] ret;
            logger.Info("Calling method ListInstalledApplications()...");
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                return key.GetSubKeyNames();
            }
        }

        public static String[] ListInstalledApplications64()
        {
            logger.Info("Calling method ListInstalledApplications64()...");
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey64))
            {              
                return key.GetSubKeyNames();
            }
        }

        public static bool IsApplictionInstalled(string displayName)
        {
            logger.Info("Calling method IsApplictionInstalled()...");
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                if (key.GetSubKeyNames().Any(keyName => key.OpenSubKey(keyName).GetValue("DisplayName").ToString() == displayName))
                    return true;
                else
                    return false;
            }
        }

        public static bool isProcessRunning(String processName)
        {
            logger.Info("Calling method isProcessRunning()...");
            Process[] pname = Process.GetProcessesByName(processName);
            if (pname.Length == 0)
              return false;
            else
              return true;
        }

        public static void killProcess(String processName)
        {
            logger.Info("Calling method killProcess()...");
            List<string> running = new List<string>();

            running = Resources.listOfProcesses();


          foreach (Process p in System.Diagnostics.Process.GetProcessesByName(processName))
            {
                try
                {
                    p.Kill();
                    p.WaitForExit(); // Possibly with a timeout
                }
                catch (Win32Exception winException)
                {                    
                    // process was terminating or can't be terminated - deal with it
                    logger.Error(winException.Message, winException);
                }
           
                catch (InvalidOperationException invalidException)
                {
                    // process has already exited - might be able to let this one go
                    logger.Error(invalidException.Message, invalidException);
                }
            }
        }

     
        
        public static List<string> listOfProcesses()
        {
            List<string> returnValue = new List<string>();

            logger.Info("Calling method listOfProcesses()...");
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                //logger.Info(String.Format("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id));

                returnValue.Add(theprocess.ProcessName);
            }

            return returnValue;
        }
    }
}
