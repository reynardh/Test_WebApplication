using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Util
{

    public static class AppSetting
    {

        static Dictionary<string, IConfiguration> Configurations = new Dictionary<string, IConfiguration>();

        /// <summary>
        ///  Get Key from appsettings.json
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ConfKey"></param>
        /// <returns></returns>
        public static string Get(string key, string ConfKey = "")
        {
            var result = Configurations[ConfKey][key];
            if (result == null)
            {
                result = Configurations[ConfKey]["AppSettings:" + key];
            }
            return result;
        }
        /// <summary>
        /// init
        /// </summary>
        static IConfiguration iconfiguration;
        public static void init(IConfiguration piconfiguration, string ConfKey = "")
        {
            iconfiguration = piconfiguration;
            if (!Configurations.ContainsKey(ConfKey))
            {
                Configurations.Add(ConfKey, iconfiguration);
            }
           
        }


    }

}
