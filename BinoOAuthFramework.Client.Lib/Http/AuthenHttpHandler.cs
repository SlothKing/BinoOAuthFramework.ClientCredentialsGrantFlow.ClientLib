using Binodata.Utility.Component.Standard.AdapterModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BinoOAuthFramework.Client.Lib.Http
{
    internal class AuthenHttpHandler
    {
        private static async Task<string> SendRequestBase(string url,string postData)
        {
            using (HttpClient client = new HttpClient())
            {
                // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
                HttpContent contentPost = new StringContent(postData, Encoding.UTF8, "application/json");
                // 發出 post 並取得結果
                HttpResponseMessage response = client.PostAsync(url, contentPost).GetAwaiter().GetResult();
                using (HttpContent content = response.Content)
                {
                    response.EnsureSuccessStatusCode();
                    byte[] httpByte = await content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    string result = Encoding.UTF8.GetString(httpByte);
                    return result;
                }
            }
        }

        /// <summary>
        /// 以POST方法發出請求
        /// </summary>
        /// <param name="clientTempEncrypt"></param>
        public static ApiResult<T> SendRequest<T>(string url, string requestString)
        {
            var result = SendRequestBase(url,requestString);

            return JsonConvert.DeserializeObject<ApiResult<T>>(result.Result);
        }
    }
}
