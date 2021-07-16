using System;
using Xunit;
using Webapi.Contact.Features.Contacts;
using MediatR;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace UnitTestContact
{
    public class GetContactTestCase
    {
     

        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        GetContactValidator _validator;
        GetContactRequest _request;
        //public GetContactTestCase(IMediator mediator,ILogger logger)
        //{
        //    _mediator = mediator;
        //    _logger = logger;
        //    _validator = new GetContactValidator();
        //    _request = new GetContactRequest();
        //}
        public GetContactTestCase()
        {
            _validator = new GetContactValidator();
            _request = new GetContactRequest();

        }
        [Theory]
        [InlineData(1.2,true)]
        [InlineData(1, true)]
        [InlineData(100, true)]
        [InlineData(-100, true)]
        public void validateGetContact(int contactId, bool isValid)
        {
            _request.contactId = contactId;
            _validator.Validate(_request).IsValid.Should().Be(isValid);
        }
        
    }
}
