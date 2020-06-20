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
        private INotify _notify;
        public SysinfoService(INotify notify)
        {
            _notify = notify; 
        }
        private const string SYSINFO_PATH = "/sysinfo";
        public async Task<SysinfoData> GetSysInfoByServerUrl(string serverName)
        {
            HttpRequest requet = new HttpRequest(serverName);
            string json = await requet.Call(SYSINFO_PATH);

            SysinfoData sysInfoData = new SysinfoData();

            dynamic dynamicJson = JValue.Parse(json);

            sysInfoData.SysInfoVersion = GetJsonValue("SysInfo Version", 
                () => { return dynamicJson.sysinfo.version; });
            
            sysInfoData.MarketingVersion = GetJsonValue("Markting Version", 
                () => { return dynamicJson["sender-ui"]["environment"]["RELEASE_VERSION"]; });
            
            sysInfoData.SenderUIVersion = GetJsonValue("Sender UI Version", 
                () => { return dynamicJson["sender-ui"]["version"]; });
            
            sysInfoData.MscVersion = GetJsonValue("MSC Version", 
                () => { return dynamicJson["msc"].version; });
            
            sysInfoData.GroovyVersion = GetJsonValue("Groovy Version", 
                () => { return dynamicJson["groovy"]["version"]; });
            
            sysInfoData.PlatformVersion = GetJsonValue("Platform Version", 
                () => { return dynamicJson["platform"]["version"]; });

            sysInfoData.SignerUIVersion = GetJsonValue("Signer UI Version", 
                () => { return dynamicJson["signer-ui"]["version"]; });

            sysInfoData.ClassicUIVersion = GetJsonValue("Classic UI Version",
                () => { return dynamicJson["classic-ui"]["version"]; });

            sysInfoData.ConnectorsVersion = GetJsonValue("Connector Version",
                () => { return dynamicJson["connectors"]["version"]; });

            sysInfoData.DocEngineVersion = GetJsonValue("Document Engine Version", 
                () => { return dynamicJson["doc-engine"]["DE Version"]; });

            sysInfoData.BackendVersion = GetJsonValue("Backend Version", 
                () => { return dynamicJson["backend"]["version"]; });

            sysInfoData.SsoServiceVersion = GetJsonValue("SSO Service Version", 
                () => { return dynamicJson["sso-service"]["version"]; });

            return sysInfoData; 
        }        

        private string GetJsonValue(string fieldName, Func<string> getValue)
        {
            try
            {
                return getValue();
            }
            catch (Exception ex)
            {
                // could not get the field name, report it. 
                _notify.WriteError($"can't get version for {fieldName}");
                return "N/A version missing or not following the correct format";
            }

        }
    }
}
