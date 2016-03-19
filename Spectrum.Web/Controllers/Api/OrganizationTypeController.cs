using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.UnitOfWork;

namespace Spectrum.Web.Controllers.Api
{
    public class OrganizationTypeController : ApiController
    {
        private CoreDbContext _context;

        public OrganizationTypeController(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _context.OrganizationTypes.ToList());
        }
    }
}
