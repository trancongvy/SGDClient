using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;

using System.Threading.Tasks;
using CDTLib;
namespace CDTControl
{
    public class Log
    {
        public string log(string companyName, string Product, string code, string _user, string _pass)
        {
            try
            {
                com.phanmemsgd.www1.Service a = new com.phanmemsgd.www1.Service();
                string k = a.GetKeyDirect(companyName, Product, code, _user, _pass);
                return k;
            }
            catch (Exception ex)
            {
                return "Lỗi";
            }
        }
        public string logWeb(string ob)
        {
            string webserver = Config.GetValue("WebServer").ToString();
            string url = webserver + "Account/LoginfromAPI";
            return Post(ob, url);
        }
        public bool Check(string user, string pass)
        {
            try
            {
                //Comboserver.Service a = new Comboserver.Service();
                com.sgdsoft.Service a = new com.sgdsoft.Service();
                bool k = a.CheckUserLogin(user, pass);
                return k;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string logFb(string ob)
        {
            string webserver = Config.GetValue("WebServer").ToString();

            string url = webserver + @"api/UserKeys";
            // string url = @"https://localhost:44347/api/UserKeys";

            return Post(ob, url);
            


        }
        public string GetExDate(string key)
        {
            string webserver = Config.GetValue("WebServer").ToString();
            string url =webserver + @"api/UserKeys";
            //  string url = @"https://localhost:44347/api/UserKeys";

            return Get(key, url);


        }
        public string RegistComputer(string ob)
        {
            string webserver = Config.GetValue("WebServer").ToString();
            string url = webserver + @"SGDAPI/api/ComputerConnections/PostComputerConnection/";
            return Post(ob, url);
        }
        public string CheckComputer(string ob)
        {
            string webserver = Config.GetValue("WebServer").ToString();
            string url =webserver + @"SGDAPI/api/ComputerConnections/PostGetComputerConnectionbyOject";
            //string url = "https://localhost:44374/api/ComputerConnections/PostGetComputerConnectionbyOject";
            return Post(ob, url);
        }
        public string GetServerAPI(string ob)
        {
            string webserver = Config.GetValue("WebServer").ToString();
            string url = webserver + @"SGDAPI/api/ServerAPIs/PostGetbyServerAPI";
            return Post(ob, url);
        }
        public string GetDataSingleFull(string ob)
        {
            string webserver = Config.GetValue("ServerAPI").ToString();
            string url = webserver + @"api/Single/GetDataFull";
            return Post(ob, url);
        }
        public string PostInSertData(string ob)
        {
            string APIserver = Config.GetValue("ServerAPI").ToString();
            string url = APIserver + @"api/Single/PostInSertData";
            return Post(ob, url);
        }

        public string PostConnect(string ob)
        {
            string APIserver = Config.GetValue("ServerAPI").ToString();
            string url = APIserver + @"api/Connect/PostConnect";
            return Post(ob, url);
        }
        public string Post(string ob, string url)
        {
            string sContentType = "application/json";
            HttpContent s = new StringContent(ob, Encoding.UTF8, sContentType);
            HttpClient oHttpClient = new HttpClient();
            try
            {
                Task<HttpResponseMessage> oTaskPostAsync = oHttpClient.PostAsync(url, s);
                if (oTaskPostAsync.Result.StatusCode == HttpStatusCode.BadRequest)
                {
                    return "";
                }
                if (oTaskPostAsync.Result.StatusCode == HttpStatusCode.OK || oTaskPostAsync.Result.StatusCode == HttpStatusCode.Created)
                {
                    return oTaskPostAsync.Result.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: true).GetAwaiter()
                        .GetResult();
                    //Get lại 

                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string Get(string key, string url)
        {
            string sContentType = "application/json";
            //HttpContent s = new StringContent(ob, Encoding.UTF8, sContentType);

            HttpClient oHttpClient = new HttpClient();
            try
            {
                var oTaskPostAsync = oHttpClient.GetAsync(url + @"/?LicenseKey=" + key);
                if (oTaskPostAsync.Result != null)
                {
                    return oTaskPostAsync.Result.Content.ReadAsStringAsync().ConfigureAwait(true).GetAwaiter().GetResult(); ;
                }

            }
            catch (Exception ex)
            {
                return "";
            }
            return "";

        }
    }
}
