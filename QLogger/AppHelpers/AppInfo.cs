using System;
using System.Reflection;
#if !NETSTANDARD2_0
using System.Deployment.Application;
#endif

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
#if !NETSTANDARD2_0
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch (InvalidDeploymentException)
            {
#endif
                return Assembly.GetEntryAssembly().GetName().Version;
#if !NETSTANDARD2_0
        }
#endif
        }
    }
}
