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

            // sysinfo
            sysInfoData.SysInfoVersion = GetJsonValue("SysInfo Version", 
                () => { return dynamicJson.sysinfo.version; });
            
            // Markerting Version 
            sysInfoData.MarketingVersion = GetJsonValue("Markting Version", 
                () => { return dynamicJson["sender-ui"]["environment"]["RELEASE_VERSION"]; });
            
            // Sender UI 
            sysInfoData.SenderUIVersion = GetJsonValue("Sender UI Version", 
                () => { return dynamicJson["sender-ui"]["version"]; });
            
            // MSC Version 
            sysInfoData.MscVersion = GetJsonValue("MSC Version", 
                () => { return dynamicJson["msc"].version; });
            
            // Groovy Version 
            sysInfoData.GroovyVersion = GetJsonValue("Groovy Version", 
                () => { return dynamicJson["groovy"]["version"]; });
            
            // Platform Version 
            sysInfoData.PlatformVersion = GetJsonValue("Platform Version", 
                () => { return dynamicJson["platform"]["version"]; });

            // Signer UI Version 
            sysInfoData.SignerUIVersion = GetJsonValue("Signer UI Version", 
                () => { return dynamicJson["signer-ui"]["version"]; });

            // Classic UI Version 
            sysInfoData.ClassicUIVersion = GetJsonValue("Classic UI Version",
                () => { return dynamicJson["classic-ui"]["version"]; });

            // Connector Verison 
            sysInfoData.ConnectorsVersion = GetJsonValue("Connector Version",
                () => { return dynamicJson["connectors"]["version"]; });

            // Document Engine Version 
            sysInfoData.DocEngineVersion = GetJsonValue("Document Engine Version", 
                () => { return dynamicJson["doc-engine"]["DE Version"]; });

            // Backend Version 
            sysInfoData.BackendVersion = GetJsonValue("Backend Version", 
                () => { return dynamicJson["backend"]["version"]; });

            // SSO Version 
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
            catch (Exception)
            {
                // could not get the field name, report it. 
                _notify.WriteError($"can't get version for {fieldName}");
                return "N/A version missing or not following the correct format";
            }

        }
    }
}
