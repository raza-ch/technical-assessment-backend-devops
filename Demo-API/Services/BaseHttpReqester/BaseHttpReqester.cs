using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo_API.Services.BaseHttpReqester
{
    public class BaseHttpReqester: IBaseHttpReqester
    {
        public readonly IHttpClientFactory _httpClientFactory;

        public BaseHttpReqester(
            IHttpClientFactory httpClientFactory
            )
        {
            this._httpClientFactory = httpClientFactory;
        }

        public Task<HttpResponseMessage> Get_Request(string url)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                }
            };

            var httpClient = this._httpClientFactory.CreateClient();
            return httpClient.SendAsync(httpRequestMessage);
        }
    }
}
