using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Spectrum.Web.Controllers.Api.Data
{
    public class CountryDataController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/App_Data/countries.json"));

            return new HttpResponseMessage()
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
