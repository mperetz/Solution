using System;
using System.Collections.Generic;
using System.Text;

namespace Oss.Versioning.Domain
{
    public class SysinfoData
    {
        public string SysInfoVersion { get; set; }
        public string SignerUIVersion { get; set; }
        public string SenderUIVersion { get; set; }
        public string MarketingVersion { get; set; }
        public string ClassicUIVersion { get; set; }
        public string MscVersion { get; set; }
        public string ConnectorsVersion { get; set; }
        public string BackendVersion { get; set; }
        public string PlatformVersion { get; set; }
        public string GroovyVersion { get; set; }
        public string DocEngineVersion { get; set; }
        public string SsoServiceVersion { get; set; }
    }
}
