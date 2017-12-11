using ITI.KDO.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class FacebookClient
    {
        public string urlBase = "https://graph.facebook.com/v2.11/";

        public async Task<IEnumerable<FacebookContact>> GetFacebookFriends(string facebookAccessToken)
        {
            JObject jsonFile;
            using (HttpClient client = new HttpClient())
            {
                HttpRequestHeaders headers = client.DefaultRequestHeaders;
                headers.Add("Authorization", string.Format("token{0}", facebookAccessToken));
                headers.Add("User-Agent", "KDO");
                HttpResponseMessage response = await client.GetAsync(urlBase + "me?fields=friends{id,first_name,last_name}");

                using (TextReader tr = new StreamReader(await response.Content.ReadAsStreamAsync()))
                using (JsonTextReader jsonReader = new JsonTextReader(tr))
                {
                    JToken json = JToken.Load(jsonReader);
                    jsonFile = json[json["friends"]].Value<JObject>();
                    JToken datas = jsonFile.SelectToken("data");
                }
            }
        }
    }
}
