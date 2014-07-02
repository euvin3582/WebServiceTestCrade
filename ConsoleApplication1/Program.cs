using System;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using TestCradle.Domain;
using System.Configuration;
using TestCradle.MTSMobileService;
using System.IO;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;

namespace TestCradle
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < int.MaxValue; i++)
            {
                testScript();
            }
            
        }

        public static void testScript()
        {
            Facade facadeObj = new Facade();
            // 0 mike, 1 kevin, 2 jwagner, 3 mobile, 4 mobile2
            facadeObj.Login = 4;
            facadeObj.AppLaunchCount = 100;
            facadeObj.DevId = "191FE6DE-F096-4248-8678B-D68F377B09C0";
            facadeObj.Options = "Mobile";
            facadeObj.Token = null;

            switch (facadeObj.Options)
            {
                case "Receiver":
                    facadeObj.Json = JSONRequestObj.ReceiverJSONObject("0");
                    break;
                case "Mobile":
                    /* "MTSMobileAuth", "MobileDeviceRegister", "CreateCase", "GenerateInvoice", "UpdateSyncTime"
                    * "InitCases", "InitInventory", "InitDoctors", "InitAddresses", "InitStatus", "InitKitAllocation", 
                    * "InitTrayTypesBySurgeryType", "UpdateTrayItemsUsage", "GetAddressesByLatLong"*/
                    facadeObj.Json = JSONRequestObj.CreateJSONObj(facadeObj, "MTSMobileAuth", "MobileDeviceRegister", "CreateCase");
                    break;
            }

            Common common = new Common();
            //FacadeLocal, FacadeDev, MTSWebServices, ReceiverLocal, ReceiverDev
            facadeObj.Connection = common.GetServiceConnectionObject("FacadeLocal");

            // Facade Service Request
            HttpWebRequest req = facadeObj.Connection;
            req.Method = "POST";
            req.Accept = "application/json";
            req.ContentType = "application/json";
            byte[] bodyBytes = Encoding.UTF8.GetBytes(facadeObj.Json);
            req.GetRequestStream().Write(bodyBytes, 0, bodyBytes.Length);
            req.GetRequestStream().Close();
            HttpWebResponse resp;
            Console.WriteLine("Request JSON:\n" + facadeObj.Json + "\n");

            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException e)
            {
                Console.WriteLine("This program is expected to throw WebException on successful run." +
                       "\n\nException Message :" + e.Message);
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                    Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                }
                resp = (HttpWebResponse)e.Response;
            }

            if (resp == null)
            {
                Console.WriteLine("Response is null");
            }
            else
            {
                Console.WriteLine("HTTP/{0} {1} {2}", resp.ProtocolVersion, (int)resp.StatusCode, resp.StatusDescription);
                foreach (string headerName in resp.Headers.AllKeys)
                {
                    Console.WriteLine("{0}: {1}", headerName, resp.Headers[headerName]);
                }
                Console.WriteLine();
                Stream respStream = resp.GetResponseStream();
                string deserializeResponse = "";

                if (respStream != null)
                {
                    facadeObj.ResponseBody = new StreamReader(respStream).ReadToEnd();

                    deserializeResponse = JsonConvert.DeserializeAnonymousType(facadeObj.ResponseBody, deserializeResponse);
                    Console.WriteLine("Response Body:\n" + deserializeResponse);
                }
                else
                {
                    Console.WriteLine("HttpWebResponse.GetResponseStream returned null");
                }

                //CreatePdfDocumentFromSerializedString(envelopeObj.ServiceQueues[0].ToString());
            }
            //Console.ReadKey();
        }

        public static void CreatePdfDocumentFromByteArray(Byte[] pdfMs)
        {
            String defaultFilePath = ConfigurationManager.AppSettings["PDFFilePath"];
            Guid guid = Guid.NewGuid();
            String fileName = defaultFilePath + guid.ToString() + ".pdf";

            using (FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                MemoryStream memoryStream = new MemoryStream(pdfMs);
                memoryStream.WriteTo(file);
                file.Close();
                memoryStream.Close();
            }
        }
        public static void CreatePdfDocumentFromSerializedString(String pdfMs)
        {
            if (String.IsNullOrEmpty(pdfMs))
                return;

            GenerateInvoice invoice = new GenerateInvoice();
            invoice = (GenerateInvoice)JsonConvert.DeserializeObject(pdfMs, invoice.GetType());

            String defaultFilePath = ConfigurationManager.AppSettings["PDFFilePath"];
            Guid guid = Guid.NewGuid();
            String fileName = defaultFilePath + guid.ToString() + ".pdf";
            byte[] bytes = Convert.FromBase64String(invoice.InvoiceString);
            FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close(); 
        }
    }
}
