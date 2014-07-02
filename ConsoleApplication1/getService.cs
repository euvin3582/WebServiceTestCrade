using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace TestCradle
{
    public class getService
    {
        public string getWebService()
        {
            string response = null;

            WebRequest req = WebRequest.Create(@"http://localhost:9915/FacadeRestServiceImpl.svc/");

            req.Method = "GET";

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);

                    response = reader.ReadToEnd();
                    Console.WriteLine(response);
                }
            }
            else
            {
                response = string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription);
                Console.WriteLine(response);
            }
            Console.Read();
            return response;
        }
    }
}
