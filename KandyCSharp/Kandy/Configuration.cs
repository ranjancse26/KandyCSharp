using System.Configuration;

namespace KandyCSharp.Kandy
{
    public static class Configuration
    {
        public static string GetKandyBaseUrl()
        {
            return ConfigurationManager.AppSettings["KandyBaseUrl"];
        }

        public static string GetDomainAPIKey()
        {
            return ConfigurationManager.AppSettings["DomainAPIKey"];
        }

        public static string GetDomainSecretKey()
        {
            return ConfigurationManager.AppSettings["DomainSecretKey"];
        }

        public static string GetApplicationAPIKey()
        {
            return ConfigurationManager.AppSettings["ApplicationAPIKey"];
        }

        public static string GetApplicationSecretKey()
        {
            return ConfigurationManager.AppSettings["ApplicationSecretKey"];
        }
    }
}
