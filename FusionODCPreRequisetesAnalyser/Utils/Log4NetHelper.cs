using log4net;
using log4net.Config;
using System;
using System.Diagnostics;

namespace FusionODCPreRequisetesAnalyser.Utils
{
    static class Log4NetHelper
    {
        private static bool _isConfigured;

        static void EnsureConfigured()
        {
            if (!_isConfigured)
            {
                // "Booting" Log4Net
                XmlConfigurator.Configure();
                _isConfigured = true;
            }
        }

        public static ILog GetLogger(Type type)
        {
            EnsureConfigured();
            log4net.ILog logger = log4net.LogManager.GetLogger(type);
            return logger;
        }
    }
}
