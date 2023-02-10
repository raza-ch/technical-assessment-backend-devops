using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Demo_API.Services.BaseHttpReqester
{
    public interface IBaseHttpReqester
    {
        public Task<HttpResponseMessage> Get_Request(string url);
    }
}
