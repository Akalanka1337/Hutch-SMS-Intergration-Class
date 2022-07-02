using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Dynamic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CYBERSCAP.Functions
{
    class SMS
    {
        public async Task<string> getHutAuthToken() //Get Hutch Auth Token (NSFW) 
        {
            string Huusername = "contact@cyberscap.com";
            string Hupassword = "tempPass1";
            string responseInString = "";
            using (var wb = new HttpClient())
            {

                wb.BaseAddress = new Uri(apiBaseURL);
                dynamic data = new ExpandoObject();
                data.username = Huusername;
                data.password = Hupassword;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                wb.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json;");
                wb.DefaultRequestHeaders.Add("Accept", "*/*");
                wb.DefaultRequestHeaders.Add("X-API-VERSION", "v1");
                var final = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                var response = await wb.PostAsync("/api/login", final);
                string hook = response.Content.ReadAsStringAsync().Result;
                dynamic readData = JObject.Parse(hook);
                responseInString = readData.accessToken;
            }
            return responseInString;
        }
    }

}
