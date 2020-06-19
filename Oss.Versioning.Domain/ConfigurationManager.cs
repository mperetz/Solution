using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Oss.Versioning.Domain
{
    public class ConfigurationData
    {
        public ConfigurationData()
        {
            QAMachines = new List<string>();
        }

        public List<String> QAMachines { get; }
    }
    public static class ConfigurationManager
    {
        public static ConfigurationData LoadConfig()
        {
            ConfigurationData config = new ConfigurationData();

            JObject jsonConfig = JObject.Parse(File.ReadAllText(@"config.json"));
            var qaMachines = jsonConfig["qa_machines"];
            foreach(var token in qaMachines.Children())
            {
                config.QAMachines.Add(token.ToString());

            }
                       
            return config; 
        }    
    }
}
