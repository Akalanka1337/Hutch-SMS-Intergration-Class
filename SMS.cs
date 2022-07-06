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
        //Put Token Here
        private string apiBaseURL = "https://bsms.hutch.lk";
        private string accessToken = "AUTH_TOKEN_HERE";
        private string champName = "Test Champ";
        private string smsMask = "Cyberscap"; 

        public async Task<string> sendSms(string numbers, string msg) //Send New SMS
        {
            string responseInString = "";
            using (var wb = new HttpClient())
            {

                wb.BaseAddress = new Uri(apiBaseURL);
                dynamic data = new ExpandoObject();
                data.campaignName = champName;
                data.mask = smsMask;
                data.numbers = numbers;
                data.content = msg;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                wb.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json;");
                wb.DefaultRequestHeaders.Add("Accept", "*/*");
                wb.DefaultRequestHeaders.Add("X-API-VERSION", "v1");
                wb.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var final = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                var response = await wb.PostAsync("/api/sendsms", final);
                string hook = response.Content.ReadAsStringAsync().Result;
                //string hook = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                dynamic readData = JObject.Parse(hook);
                if ((int)response.StatusCode == 200 && readData.serverRef != null)
                {
                    responseInString = "Sent";
                }
                else
                {
                    responseInString = "Failed";
                }
            }
            return responseInString;
        }

    }
}
