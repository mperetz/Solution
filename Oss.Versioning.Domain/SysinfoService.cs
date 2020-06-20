using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Json.Net;
using Newtonsoft.Json.Linq;

namespace Oss.Versioning.Domain
{

    class SysinfoService
    {
        private const string SYSINFO_PATH = "/sysinfo";
        public async Task<SysinfoData> GetSysInfoByServerUrl(string serverName)
        {
            HttpRequest requet = new HttpRequest(serverName);
            string json = await requet.Call(SYSINFO_PATH);

            SysinfoData sysInfoData = new SysinfoData();

            dynamic dynamicJson = JValue.Parse(json);
            sysInfoData.SysInfoVersion = dynamicJson.sysinfo.version;
            sysInfoData.MarketingVersion = dynamicJson["sender_ui"].enviornment.RELEASE_VERSION;
            sysInfoData.SenderUIVersion = dynamicJson["sender_ui"].version;
            sysInfoData.MscVersion = dynamicJson["msc"].version;
            sysInfoData.GroovyVersion = dynamicJson["groovy"].version;
            sysInfoData.PlatformVersion = dynamicJson["platform"].version;
            sysInfoData.SignerUIVersion = dynamicJson["signer-ui"].version;

            return sysInfoData; 
        }        
    }
}
