using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.Serialization;

namespace TestCradle.Domain
{
    [DataContract]
    public class ServiceQueue
    {
        [DataMember(Name = "MTSMobileAuth")]
        [JsonProperty(Required = Required.Default, NullValueHandling=NullValueHandling.Ignore)]
        public string MTSMobileAuth { get; set; }

        [DataMember(Name = "MobileDeviceRegister")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MobileDeviceRegister { get; set; }

        [DataMember(Name = "InitCases")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string InitCases { get; set; }

        [DataMember(Name = "InitInventory")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string InitInventory { get; set; }

        [DataMember(Name = "InitDoctors")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string InitDoctors { get; set; }

        [DataMember(Name = "InitAddresses")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string InitAddresses { get; set; }

        [DataMember(Name = "InitStatus")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string InitStatus { get; set; }

        [DataMember(Name = "InitKitAllocation")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string InitKitAllocation { get; set; }

        [DataMember(Name = "InitTrayTypesBySurgeryType")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string InitTrayTypesBySurgeryType { get; set; }

        [DataMember(Name = "GetAddressesByLatLong")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string GetAddressesByLatLong { get; set; }

        [DataMember(Name = "CreateCase")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string CreateCase { get; set; }

        [DataMember(Name = "UpdateTrayItemsUsage")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string UpdateTrayItemsUsage { get; set; }

        [DataMember(Name = "GenerateInvoice")]
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string GenerateInvoice { get; set; }

    }
}
