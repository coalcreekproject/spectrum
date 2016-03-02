using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using Spectrum.Data.Eoc.Infrastructure;
using Spectrum.Data.Eoc.Models;

namespace Spectrum.Web.Areas.Eoc.Controllers.Api
{
    public class IncidentManagementController : ApiController
    {
        private readonly string _authKey = ConfigurationManager.AppSettings["authKey"];
        private readonly string _collectionId = ConfigurationManager.AppSettings["incidents-collection"];
        private readonly string _databaseId = ConfigurationManager.AppSettings["eoc-database"];
        private readonly string _endpoint = ConfigurationManager.AppSettings["endpoint"];

        public IEnumerable<Incident> Get()
        {
            var repo = new DocumentDbRepository<Incident>(_endpoint,
                _authKey,
                _databaseId,
                _collectionId);

            var incidents = repo.GetItems();
            return incidents;
        }
    }
}