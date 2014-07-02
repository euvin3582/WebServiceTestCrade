using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestCradle.Domain
{
    [DataContract]
    public class JSON
    {
        [DataMember(Name = "CoID")]
        public string CoID { get; set; }

        [DataMember(Name = "RepID")]
        public string RepID { get; set; }

        [DataMember(Name = "Role")]
        public string Role { get; set; }

        [DataMember(Name = "DevID")]
        public string DevID { get; set; }

        [DataMember(Name = "AppID")]
        public string AppID { get; set; }

        [DataMember(Name = "SyncRequestTime")]
        public string SyncRequestTime { get; set; }

        [DataMember(Name = "SyncResponseTime")]
        public string SyncResponseTime { get; set; }

        [DataMember(Name = "LocationVector")]
        public string LocationVector { get; set; }

        [DataMember(Name = "MtsToken")]
        public string MtsToken { get; set; }

        [DataMember(Name = "AppLaunchCount")]
        public string AppLaunchCount { get; set; }

        [DataMember(Name = "SQL")]
        public string SQL { get; set; }

        [DataMember(Name = "ServiceQueue")]
        public ServiceQueue[] ServiceQueues { get; set; }

        [DataMember(Name = "Command")]
        public ArrayList Command { get; set; }

        [DataMember(Name = "Commit")]
        public string Commit { get; set; }
    }
}
