using MTSUtilities.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace TestCradle
{
    public class Common
    {
        public HttpWebRequest GetServiceConnectionObject(string connectionName)
        {
            string con = ConfigurationManager.AppSettings[connectionName];

            if(!String.IsNullOrEmpty(con))
                return (HttpWebRequest)HttpWebRequest.Create(con);
            else 
                return null;
        }
        
    }
}
