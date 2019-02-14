using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(ClassLibrary1.Startup))]

namespace ClassLibrary1
{
    public class Startup
    {
        public static int GetIntConfiguration(string keyName)
        {
            string value = ConfigurationManager.AppSettings[keyName] ?? "0";
            int theValue = 0;
            if (int.TryParse(value, out theValue))
            {
                return theValue;
            }
            return -1;
        }
    }
}
