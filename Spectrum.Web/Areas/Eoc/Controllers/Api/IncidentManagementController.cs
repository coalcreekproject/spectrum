using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Spectrum.Data.Eoc.Infrastructure;
using Spectrum.Data.Eoc.Models;

namespace Spectrum.Web.Areas.Eoc.Controllers.Api
{
    public class IncidentManagementController : ApiController
    {
        public IncidentManagementController()
        {
            _dbRepository = new DocumentDbRepository<Incident>(_endpoint,
                _authKey,
                _databaseId,
                _collectionId);
        }

        private readonly string _authKey = ConfigurationManager.AppSettings["authKey"];
        private readonly string _collectionId = ConfigurationManager.AppSettings["incidents-collection"];
        private readonly string _databaseId = ConfigurationManager.AppSettings["eoc-database"];
        private readonly DocumentDbRepository<Incident> _dbRepository;
        private readonly string _endpoint = ConfigurationManager.AppSettings["endpoint"];

        [Route("Eoc/api/Incidents")]
        public IEnumerable<Incident> GetIncident()
        {
            var incidents = _dbRepository.GetItems()
                .Where(inc => inc.OrganizationId == 1 && inc.UserId == 1);
            return incidents;
        }

        [Route("Eoc/api/Incidents")]
        [HttpPut]
        public async Task<IHttpActionResult> PutIncident(Incident incident)
        {
            if (incident == null)
            {
                return BadRequest("Incident data is invalid.");
            }

            try
            {
                // TODO: Hook into user utility for current user
                incident.OrganizationId = 1;
                incident.UserId = 1;
                var document = await _dbRepository.UpdateItemAsync(incident.Id, incident);
                return Ok(document.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Eoc/api/Incidents")]
        [HttpPost]
        public async Task<IHttpActionResult> PostIncident(Incident incident)
        {
            if (incident == null)
            {
                return BadRequest("Incident data is invalid.");
            }

            try
            {
                // TODO: Hook into user utility for current user
                incident.OrganizationId = 1;
                incident.UserId = 1;
                var document = await _dbRepository.CreateItemAsync(incident);
                return Ok(document.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Eoc/api/Incidents/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteIncident(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Incident data is invalid.");
            }

            try
            {
                var response = await _dbRepository.DeleteItemAsync(id);
                return Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}