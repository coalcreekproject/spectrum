using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Eoc.Infrastructure;
using Spectrum.Data.Eoc.Models;
using Spectrum.Logic.Identity;
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

            var userId = Convert.ToInt32(User.Identity.GetUserId());

            //TODO: Stop using this there is WebAPI Service for this, has been for months
            var currentUser = UserUtility.GetUserFromMemoryCache(userId);

            if (currentUser == null)
            {
                throw new ApplicationException("Unable to get the current user from cache.");
            }

            if (currentUser.SelectedOrganizationId == 0)
            {
                var firstOrgFound = currentUser.UserOrganizations.FirstOrDefault();
                if (firstOrgFound == null)
                {
                    throw new ApplicationException("Unable to set the user's OrganizationId.");
                }
                currentUser.SelectedOrganizationId = firstOrgFound.OrganizationId;
            }

            _currentOrganizationId = currentUser.SelectedOrganizationId;
            _currentUserId = currentUser.Id;
        }

        private readonly string _authKey = ConfigurationManager.AppSettings["authKey"];
        private readonly string _collectionId = ConfigurationManager.AppSettings["incidents-collection"];
        private readonly int _currentOrganizationId;
        private readonly int _currentUserId;
        private readonly string _databaseId = ConfigurationManager.AppSettings["eoc-database"];
        private readonly DocumentDbRepository<Incident> _dbRepository;
        private readonly string _endpoint = ConfigurationManager.AppSettings["endpoint"];

        [Route("~/Eoc/api/Incident/{id:guid}")]
        [HttpGet]
        public IHttpActionResult GetIncident(Guid id)
        {
            var incident = _dbRepository.GetItem(item => item.Id == id.ToString());
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

        [Route("~/Eoc/api/Incident/{incidentId:guid}/Log/{logId:int}")]
        [HttpGet]
        public IHttpActionResult GetIncidentLog(Guid incidentId, int logId)
        {
            var incident = _dbRepository.GetItem(item => item.Id == incidentId.ToString());
            if (incident == null)
            {
                return NotFound();
            }

            if (incident.Logs == null || !incident.Logs.Any())
            {
                return BadRequest($"Incident {incidentId} does not contain any logs.");
            }

            var incidentLog = incident.Logs.SingleOrDefault(log => log.LogId == logId);
            if (incidentLog == null)
            {
                return BadRequest($"Unable to find log with Id: {logId}");
            }

            return Ok(incidentLog);
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<Incident> GetIncidents()
        {
            var incidents = _dbRepository.GetItems()
                .Where(inc => inc.OrganizationId == _currentOrganizationId && inc.UserId == _currentUserId)
                .OrderByDescending(inc => inc.CreateDate);
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
                // Have to do a lookup of document or we lose any logs, or
                // we could alternatively pass in the logs
                // Investigate if we can update only certain fields of a document and
                // not the document as a whole
                var document = _dbRepository.GetItem(doc => doc.Id == incident.Id);
                if (document == null)
                {
                    return BadRequest("Unable to find document to update: See PutIncident action.");
                }

                document.IncidentName = incident.IncidentName;
                document.Level = incident.Level;
                document.Status = incident.Status;
                document.Type = incident.Type;

                var documentUpdated = await _dbRepository.UpdateItemAsync(document.Id, document);
                return Ok(documentUpdated.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("~/Eoc/api/Incident/Log")]
        [HttpPut]
        public async Task<IHttpActionResult> PutIncidentLog(IncidentLogInputViewModel incidentLog)
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
                    return BadRequest("Unable to find document to update: See PutIncidentLog action.");
                }
                var savedDocId = await EditPositionLog(document, incidentLog);
                return Ok(savedDocId);
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
                incident.OrganizationId = _currentOrganizationId;
                incident.UserId = _currentUserId;
                incident.CreateDate = DateTime.UtcNow;

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
                var savedDocId = await InsertPositionLog(document, incidentLog);
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

        [Route("~/Eoc/api/Incident/{incidentId:guid}/Log/{logId:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteIncidentLog(Guid incidentId, int logId)
        {
            if (logId == 0 || incidentId == Guid.Empty)
            {
                return BadRequest("Incident log delete data is invalid.");
            }

            try
            {
                var document = _dbRepository.GetItem(doc => doc.Id == incidentId.ToString());
                if (document == null)
                {
                    return BadRequest("Unable to find document to update: See DeleteIncidentLog action.");
                }
                var savedDocId = await DeletePositionLog(document, logId);
                return Ok(savedDocId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> InsertPositionLog(Incident document, IncidentLogInputViewModel incidentLog)
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

        private async Task<string> EditPositionLog(Incident document, IncidentLogInputViewModel incidentLog)
        {
            // Find the log entry
            var logToEdit = document.Logs?.SingleOrDefault(log => log.LogId == incidentLog.Log.LogId);

            if (logToEdit == null)
            {
                return null;
            }

            // Edit the properties
            logToEdit.LogDate = incidentLog.Log.LogDate;
            logToEdit.LogEntry = incidentLog.Log.LogEntry;
            logToEdit.LogName = incidentLog.Log.LogName;
            logToEdit.LogTitle = incidentLog.Log.LogTitle;

            var savedDoc = await _dbRepository.UpdateItemAsync(document.Id, document);
            return savedDoc.Id;
        }

        private async Task<string> DeletePositionLog(Incident document, int logId)
        {
            // Remove the log entry
            var logToRemove = document.Logs?.SingleOrDefault(log => log.LogId == logId);

            if (logToRemove == null)
            {
                return null;
            }

            document.Logs.Remove(logToRemove);
            var savedDoc = await _dbRepository.UpdateItemAsync(document.Id, document);
            return savedDoc.Id;
        }

        /*
         * Significant Events
         */


        [Route("~/Eoc/api/Incident/{incidentId:guid}/Event/{eventId:int}")]
        [HttpGet]
        public IHttpActionResult GetIncidentEvent(Guid incidentId, int eventId)
        {
            var incident = _dbRepository.GetItem(item => item.Id == incidentId.ToString());

            if (incident == null)
            {
                return NotFound();
            }

            if (incident.Events == null || !incident.Events.Any())
            {
                return BadRequest($"Incident {incidentId} does not contain any significant events.");
            }

            var incidentEvent = incident.Events.SingleOrDefault(e => e.EventId  == eventId);

            if (incidentEvent == null)
            {
                return BadRequest($"Unable to find event with Id: {eventId}");
            }

            return Ok(incidentEvent);
        }

        [Route("~/Eoc/api/Incident/Event")]
        [HttpPut]
        public async Task<IHttpActionResult> PutIncidentEvent(IncidentEventInputViewModel incidentEvent)
        {
            if (string.IsNullOrEmpty(incidentEvent.Id) || incidentEvent.Event == null)
            {
                return BadRequest("Incident log data is invalid.");
            }

            try
            {
                var document = _dbRepository.GetItem(doc => doc.Id == incidentEvent.Id);
                if (document == null)
                {
                    return BadRequest("Unable to find document to update: See PutIncidentLog action.");
                }
                var savedDocId = await EditEvent(document, incidentEvent);
                return Ok(savedDocId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("~/Eoc/api/Incident/Event")]
        [HttpPost]
        public async Task<IHttpActionResult> PostIncidentEvent(IncidentEventInputViewModel incidentEvent)
        {
            if (string.IsNullOrEmpty(incidentEvent.Id) || incidentEvent.Event == null)
            {
                return BadRequest("Incident log data is invalid.");
            }

            try
            {
                var document = _dbRepository.GetItem(doc => doc.Id == incidentEvent.Id);
                if (document == null)
                {
                    return BadRequest("Unable to find document to update: See PostIncidentLog action.");
                }
                var savedDocId = await InsertEvent(document, incidentEvent);
                return Ok(savedDocId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("~/Eoc/api/Incident/{incidentId:guid}/Event/{eventId:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteIncidentEvent(Guid incidentId, int eventId)
        {
            if (eventId == 0 || incidentId == Guid.Empty)
            {
                return BadRequest("Incident log delete data is invalid.");
            }

            try
            {
                var document = _dbRepository.GetItem(doc => doc.Id == incidentId.ToString());
                if (document == null)
                {
                    return BadRequest("Unable to find document to update: See DeleteIncidentEvent action.");
                }

                var savedDocId = await DeleteEvent(document, eventId);

                return Ok(savedDocId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Helpers I reckon
        private async Task<string> InsertEvent(Incident document, IncidentEventInputViewModel incidentEvent)
        {
            const int eventId = 1;
            if (document.Events != null)
            {
                // Find the max LogId value and +1
                var maxId = document.Events.Max(e => e.EventId);

                incidentEvent.Event.EventId = maxId + eventId;
            }
            else
            {
                // Add Events to document and insert an event
                document.Events = new List<Event>();
                incidentEvent.Event.EventId = eventId;
            }

            document.Events.Add(incidentEvent.Event);
            var savedDoc = await _dbRepository.UpdateItemAsync(incidentEvent.Id, document);
            return savedDoc.Id;
        }

        private async Task<string> EditEvent(Incident document, IncidentEventInputViewModel incidentEvent)
        {
            // Find the log entry
            var eventToEdit = document.Events?.SingleOrDefault(e => e.EventId == incidentEvent.Event.EventId);

            if (eventToEdit == null)
            {
                return null;
            }

            // Edit the properties
            eventToEdit.EventDate = incidentEvent.Event.EventDate;
            eventToEdit.EventEntry = incidentEvent.Event.EventEntry;
            eventToEdit.EventName = incidentEvent.Event.EventName;
            eventToEdit.EventTitle = incidentEvent.Event.EventTitle;

            var savedDoc = await _dbRepository.UpdateItemAsync(document.Id, document);
            return savedDoc.Id;
        }

        private async Task<string> DeleteEvent(Incident document, int eventId)
        {
            // Remove the log entry
            var eventToRemove = document.Events?.SingleOrDefault(e => e.EventId == eventId);

            if (eventToRemove == null)
            {
                return null;
            }

            document.Events.Remove(eventToRemove);
            var savedDoc = await _dbRepository.UpdateItemAsync(document.Id, document);
            return savedDoc.Id;
        }
    }
}