using System;
using System.Collections.Generic;
using System.Text;

namespace Oss.Versioning.Domain
{
    /// <summary>
    /// Grabs ths sysinfo from all the QA machines. 
    /// </summary>
    public class QAMachineCollector
    {
        private readonly INotify _notify; 
        public QAMachineCollector(INotify notify)
        {
            _notify = notify;                    
        }

        public async void Collect()
        {           
            // service to get the sysinfo
            SysinfoService sysInfoService = new SysinfoService();
            
            // load the configuration, with all the servers we need to check
            var configuration = ConfigurationManager.LoadConfig();
            
            // for each server, get the sysinfo and report information on it 
            foreach (var url in configuration.QAMachines)
            {
                try
                {
                    var sysinfo = await sysInfoService.GetSysInfoByServerUrl(url);
                    UpdateResults(url, sysinfo);
                }
                catch (Exception ex)
                {
                    _notify.WriteError($"error processing: {url}. Exception {ex.Message}");
                }
            }
        }

        public void UpdateResults(string url, SysinfoData data)
        {
            _notify.Write($"QA Machine: {url} Running: {data.MarketingVersion}");
            
            _notify.Write($"Back-end version: {data.BackendVersion}");
            _notify.Write($"Classic-UI version: {data.ClassicUIVersion}");
            _notify.Write($"Connectors Version: {data.ConnectorsVersion}");
            _notify.Write($"Doc Engine Version: {data.DocEngineVersion}");
            _notify.Write($"Groovy Version: {data.GroovyVersion}");
            _notify.Write($"MSC Version: {data.MscVersion}");
            _notify.Write($"Platform Version: {data.PlatformVersion}");
            _notify.Write($"Sender UI Version: {data.SenderUIVersion}");
            _notify.Write($"Signer UI Version: {data.SignerUIVersion}");
            _notify.Write($"SSO Version: {data.SsoServiceVersion}");
            _notify.Write($"Sysinfo Version: {data.SysInfoVersion}");
        }       
    }
}
