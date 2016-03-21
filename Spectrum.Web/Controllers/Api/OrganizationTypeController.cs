using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Web.Models;

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
            var organizationTypes = _context.OrganizationTypes.ToList();

            return Request.CreateResponse(HttpStatusCode.OK, 
                organizationTypes.Select(Mapper.Map<OrganizationTypeViewModel>).ToList());
        }
    }
}
