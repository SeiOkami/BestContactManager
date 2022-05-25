using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Contacts.DesctopClient.Identity
{
    public class HttpClientAPI : HttpClient
    {
        public void SetBearerToken(string token)
        {
            ((HttpClient)this).SetBearerToken(token);
        }

        public HttpClientAPI(string? token = null):base()
        {
            if (token != null)
                SetBearerToken(token);
        }

    }
}
