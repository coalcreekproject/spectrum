using System.Collections.Generic;
using System.Web.Http;

namespace Spectrum.Web.Controllers.WebApiControllers
{
    public class OrganizationProfilesController : ApiController
    {
        // GET: api/OrganizationProfiles
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/OrganizationProfiles/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/OrganizationProfiles
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/OrganizationProfiles/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/OrganizationProfiles/5
        public void Delete(int id)
        {
        }
    }
}
