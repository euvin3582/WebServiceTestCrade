using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.Text;

namespace MTSFacade
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFacade" in both code and config file together.
    [ServiceContract]
    public interface IFacade
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string InBox();
    }
}
