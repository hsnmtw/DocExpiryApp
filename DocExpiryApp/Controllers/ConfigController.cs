using System;
using System.Configuration;

namespace DocExpiryApp.Controllers
{
    public class ConfigController
    {
        public string this[string name]
        {
            get
            {
                var value = System.Configuration.ConfigurationManager.AppSettings[name];
                return value==null ? name : value;
            }
        }

        private static ConfigController _instance = new ConfigController();
        public static ConfigController Instance { get { return _instance; } }

        private ConfigController(){}
    }
}