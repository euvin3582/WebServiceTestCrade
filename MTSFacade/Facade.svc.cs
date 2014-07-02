using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace MTSFacade
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Facade" in code, svc and config file together.
    public class Facade : IFacade
    {
        public void DoWork()
        {
        }

        public string InBox()
        {
            string x = OperationContext.Current.RequestContext.RequestMessage.ToString();
            String CoID = "";
            String RepID = "";

            XmlDocument payload = new XmlDocument();
            payload.LoadXml(x);
            XmlNode nCoID = payload.SelectSingleNode("//CoID");
            XmlNode nRepID = payload.SelectSingleNode("//RepID");

            if (nCoID != null)
                CoID = nCoID.InnerText;
            if (nRepID != null)
                RepID = nRepID.InnerText;

            return "Your requested CoID is: " + CoID + ". Your requested RepID is: " + RepID;
        }
    }
}
