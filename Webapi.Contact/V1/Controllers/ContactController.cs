
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using Webapi.Contact.Helpers;
using Webapi.Contact.Features.Contacts;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Webapi.Contact.Controllers
{
    [ApiVersion("1")]
    // [Route("v{version:apiVersion}/[controller]")]
    [Route("V1/[controller]")]
    [ApiController]
   // [ServiceFilter(typeof(CustomAuthorize))]
    public class ContactController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<ContactController> _logger;


        public ContactController(IMediator mediator,ILogger<ContactController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        // GET: api/<ContactController>
        [HttpGet]
        [Route("{ContactId}")]
        public async Task<GetContactResponse> Get(int ContactId)
        {
            GetContactRequest request = new GetContactRequest() { contactId= ContactId };
            return await _mediator.Send(request).ConfigureAwait(false);
        }

        //// GET api/<ContactController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ContactController>
        [HttpPost]
        public async Task<InsertContactResponse> Post([FromBody] InsertContactRequest request)
        {
            return await _mediator.Send(request).ConfigureAwait(false);
        }

        // PUT api/<ContactController>/5
        [HttpPut]
        public async Task<UpdateContactResponse> Put( [FromBody] UpdateContactRequest request)
        {
            return await _mediator.Send(request).ConfigureAwait(false);
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{Contactid}")]
        public async Task<DeleteContactResponse> Delete(int Contactid)
        {
            DeleteContactRequest request = new DeleteContactRequest() { ContactId = Contactid };
            return await _mediator.Send(request).ConfigureAwait(false);
        }
    }
}
