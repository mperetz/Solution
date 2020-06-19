using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Versioning.Domain
{
    public class HttpRequest
    {
        private string _url; 
        
        public HttpRequest(string url)
        {
            _url = url; 
        }

        public async Task<string> Call(string path)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(path);
                return await response.Content.ReadAsStringAsync();
            }
        }

     
    
    }
}
