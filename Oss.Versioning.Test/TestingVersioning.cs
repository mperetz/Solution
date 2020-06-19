using Microsoft.VisualStudio.TestTools.UnitTesting;

using Oss.Versioning.Domain;
using System;
using System.Threading.Tasks;
using Json.Net;
using Newtonsoft.Json.Linq;


namespace Oss.Versioning.Test
{
    [TestClass]
    public class TestingVersioning
    {
        [TestMethod]
        public void TestConfig()
        {
            var config = ConfigurationManager.LoadConfig();



        }
        [TestMethod]
        public async Task TestGettingManifest()
        {
            HttpRequest request = new HttpRequest("https://ossq1.rnd.esignlive.com");
            var output = await request.Call("/sysinfo");
            Console.WriteLine(output);

            try
            {
                dynamic jsonResponse = JValue.Parse(output);

                var x4 = jsonResponse["sender-ui.environment.RELEASE_VERSION"];
                var x1 = jsonResponse["sender-ui"]["environment"]["RELEASE_VERSION"];
                var x = jsonResponse.sysinfo["sender-ui"].enviornment.RELEASE_VERSION;
            }
            catch (Exception ex)
            {


            }




            Assert.IsTrue(true);

        }
    }
}
