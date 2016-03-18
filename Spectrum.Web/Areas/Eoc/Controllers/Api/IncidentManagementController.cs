﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Spectrum.Data.Eoc.Infrastructure;
using Spectrum.Data.Eoc.Models;
using Spectrum.Web.Models;

namespace Spectrum.Web.Areas.Eoc.Controllers.Api
{
    [RoutePrefix("Eoc/api/Incidents")]
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

        [Route("~/Eoc/api/Incident/{id}")]
        [HttpGet]
        public IHttpActionResult GetIncident(string id)
        {
            var incident = _dbRepository.GetItem(item => item.Id == id);
            if (incident == null)
            {
                return NotFound();
            }

            if (incident.Logs != null)
            {
                incident.Logs = incident.Logs.OrderByDescending(log => log.LogDate).ToList();
                return Ok(incident);
            }

            return Ok(incident);
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<Incident> GetIncidents()
        {
            var incidents = _dbRepository.GetItems()
                .Where(inc => inc.OrganizationId == 1 && inc.UserId == 1);
            return incidents;
        }

        [Route("")]
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

        [Route("")]
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

        [Route("~/Eoc/api/Incident/Log")]
        [HttpPost]
        public async Task<IHttpActionResult> PostIncidentLog(IncidentLogInputViewModel incidentLog)
        {
            if (string.IsNullOrEmpty(incidentLog.Id) || incidentLog.Log == null)
            {
                return BadRequest("Incident log data is invalid.");
            }

            try
            {
                var document = _dbRepository.GetItem(doc => doc.Id == incidentLog.Id);
                if (document == null)
                {
                    return BadRequest("Unable to find document to update: See PostIncidentLog action.");
                }
                var savedDocId = await InsertPostionLog(document, incidentLog);
                return Ok(savedDocId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
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

        private async Task<string> InsertPostionLog(Incident document, IncidentLogInputViewModel incidentLog)
        {
            const int logId = 1;
            if (document.Logs != null)
            {
                // Find the max LogId value and +1
                var maxId = document.Logs.Max(log => log.LogId);
                incidentLog.Log.LogId = maxId + logId;
            }
            else
            {
                // Add Logs to document and insert a log
                document.Logs = new List<Log>();
                incidentLog.Log.LogId = logId;
            }

            document.Logs.Add(incidentLog.Log);
            var savedDoc = await _dbRepository.UpdateItemAsync(incidentLog.Id, document);
            return savedDoc.Id;
        }
    }
}