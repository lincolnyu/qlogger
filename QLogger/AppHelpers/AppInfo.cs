using System;
using System.Reflection;
using System.Deployment.Application;

namespace QLogger.AppHelpers
{
    public static class AppInfo
    {
        public static string GetAppExecutableName()
        {
            return Assembly.GetEntryAssembly().GetName().Name;
        }

        public static Version GetAppVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch (InvalidDeploymentException)
            {
                return Assembly.GetEntryAssembly().GetName().Version;
            }
        }
    }
}
