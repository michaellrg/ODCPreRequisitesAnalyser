using FusionODCPreRequisetesAnalyser.Applications;
using FusionODCPreRequisetesAnalyser.Utils;
using log4net;
using log4net.Config;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Configuration;

namespace FusionODCPreRequisetesAnalyser
{
    class Program
    {
        private static readonly ILog logger = Log4NetHelper.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static String filePathSuperPackage = ConfigurationManager.AppSettings["File-Path-Unify-SuperPackage"];

        static void Main(string[] args)
        {
            // Do all the stuff here
                
            //Resources.listOfProcesses();
            

            List<string> running = new List<string>();

            running = Resources.listOfProcesses();

            Applications.killProcess.Kill("OUTLOOK");
            Applications.killProcess.Kill("COMMUNICATOR");
            
    
            String[] installed = Resources.ListInstalledApplications();

            List<string> installedNames = new List<string>();

            for (int i =0 ; i < installed.Length; i++)
            {
                
                Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + installed[i]);

                string name = (string) key.GetValue("DisplayName");

                installedNames.Add(name);

                logger.Info(String.Format("Installed Application: {0}, {1}", name, installed[i]));

            }

            installed = Resources.ListInstalledApplications64();
                        
            for (int i = 0; i < installed.Length; i++)
            {
                

                Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\" + installed[i]);

                string name = (string)key.GetValue("DisplayName");

                installedNames.Add(name);

                logger.Info(String.Format("Installed Application: {0}, {1}", name, installed[i]));
            }

            bool CPlusPlus2008 = false;
            bool DotNet35 = false;
            bool WSE20 = false;
            bool WSE30 = false;
            bool CPlusPlus2005 = false;
            bool CPlusPlus2010x86 = false;
            bool CPlusPlus2010x64 = false;

            foreach (String appName in installedNames)
            {
                try
                {
                    if (appName.Contains("Microsoft Visual C++ 2008 Redistributable - x86"))
                    {
                        CPlusPlus2008 = true;
                    }
                    if (appName.Contains("Microsoft .NET Framework 3.5"))
                    {
                        DotNet35 = true;
                    }
                    if (appName.Contains("Microsoft WSE 2.0 SP3"))
                    {
                        WSE20 = true;
                    }
                    if (appName.Contains("Microsoft-WSE-3.0"))
                    {
                        WSE30 = true;
                    }
                    if (appName.Contains("Microsoft Visual C++ 2005 Redistributable"))
                    {
                        CPlusPlus2005 = true;
                    }
                    if (appName.Contains("Microsoft Visual C++ 2010  x86 Redistributable"))
                    {
                        CPlusPlus2010x86 = true;
                    }
                    if (appName.Contains("Microsoft Visual C++ 2010  x64 Redistributable"))
                    {
                        CPlusPlus2010x64 = true;
                    }
                }
                catch (System.Exception ex) { }
            }

            logger.Info(String.Format("1"));
             // Perform setup MS Visual C++ 2008

            if(!CPlusPlus2008){
                String filePath = ConfigurationManager.AppSettings["File-Path-Microsoft-Visual-C++-2008-Redistributable"];
                logger.Info(String.Format("File-Path-Microsoft-Visual-C++-2008-Redistributable: {0}", filePathSuperPackage + filePath));
                Applications.startSetup.callInstallation(filePathSuperPackage + filePath,true, "/q");
                
            }

            logger.Info(String.Format("2"));
            // Perform setup MS WSE 2.0
            if (!WSE20)
            {
                String filePath = ConfigurationManager.AppSettings["File-Path-Microsoft-WSE-2.0-SP3-Runtime"];

                logger.Info(String.Format("File-Path-Microsoft-WSE-2.0-SP3-Runtime: {0}", filePathSuperPackage + filePath));
                Applications.startSetup.callInstallation("msiexec.exe", true, "/i\"" + filePathSuperPackage + filePath + "\" /passive");   
            }
            if (!WSE30)
            {
                String filePath = ConfigurationManager.AppSettings["File-Path-Microsoft-WSE-3.0"];

                logger.Info(String.Format("File-Path-Microsoft-WSE-3.0{0}", filePathSuperPackage + filePath));
                Applications.startSetup.callInstallation("msiexec.exe", true, "/i\"" + filePathSuperPackage + filePath + "\" /passive");
            }


            logger.Info(String.Format("3"));
            // Perform setup C++ 2005
            if (!CPlusPlus2005)
            {
                String filePath = ConfigurationManager.AppSettings["File-Path-Microsoft-Visual-C++-2005-Redistributable"];

                logger.Info(String.Format("File-Path-Microsoft-Visual-C++-2005-Redistributable: {0}", filePathSuperPackage + filePath));
                Applications.startSetup.callInstallation(filePathSuperPackage +  filePath, true, "/q");
                  }

            logger.Info(String.Format("4"));
            // Perform setup C++ 2010 x86
            if (!CPlusPlus2010x86)
            {
                String filePath = ConfigurationManager.AppSettings["File-Path-Microsoft-Visual-C++-2010-x86-Redistributable"];

                logger.Info(String.Format("File-Path-Microsoft-Visual-C++-2010-x86-Redistributable: {0}", filePathSuperPackage + filePath));
                Applications.startSetup.callInstallation(filePathSuperPackage + filePath ,true, "/q");
                
            }

            logger.Info(String.Format("5"));
            // Perform setup C++ 2010 x64
            if (!CPlusPlus2010x64)
            {
                String filePath = ConfigurationManager.AppSettings["File-Path-Microsoft-Visual-C++-2010-x64-Redistributable"];

                logger.Info(String.Format("File-Path-Microsoft-Visual-C++-2010-x64-Redistributable: {0}", filePathSuperPackage + filePath));
                Applications.startSetup.callInstallation(filePathSuperPackage + filePath ,true, "/q");
                
            }

            logger.Info(String.Format("6"));
            //Perform setup .net framework 3.5
            if (!DotNet35)
            {
                String filePath = ConfigurationManager.AppSettings["File-Path-Microsoft-.NET-Framework-3.5-SP1"];

                logger.Info(String.Format("File-Path-Microsoft-.NET-Framework-3.5-SP1: {0}", filePathSuperPackage + filePath));
                Applications.startSetup.callInstallation(filePathSuperPackage + filePath, true  , "/q");
            }

            logger.Info(String.Format("7"));

            //Delete %appdata \ siemens \ openscape
            String filePath1 = ConfigurationManager.AppSettings["File-Path-Delete-Siemens"];
            logger.Info(String.Format("File-Path-Delete-Siemens: {0}", filePathSuperPackage + filePath1));
            Applications.startSetup.callInstallation(filePathSuperPackage + filePath1, true, null);
            
            logger.Info(String.Format("8"));
            
            //Super PackagePackage
            filePath1 = ConfigurationManager.AppSettings["File-Path-Unify-SuperPackagePackage"];
            logger.Info(String.Format("File-Path-Unify-SuperPackagePackage: {0}", filePathSuperPackage + filePath1));
            Applications.startSetup.callInstallation(filePathSuperPackage + filePath1, true, null);
            
            logger.Info(String.Format("9"));
            //Delete Setup
            filePath1 = ConfigurationManager.AppSettings["File-Path-Delete-Setup"];
            logger.Info(String.Format("File-Path-Delete-Setup: {0}", filePathSuperPackage + filePath1));
            Applications.startSetup.callInstallation(filePathSuperPackage + filePath1, true, null);

            logger.Info(String.Format("Finished Installation"));
        }  
    }
}
