using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestCradle.Domain
{
     [DataContract]
    public class GenerateInvoice
    {
         [DataMember(Name = "GenerateInvoice")]
         public string InvoiceString { get; set; }
    }
}
