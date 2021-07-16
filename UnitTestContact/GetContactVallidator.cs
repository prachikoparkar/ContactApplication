using System;
using Xunit;
using Webapi.Contact.Features.Contacts;
using MediatR;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace UnitTestContact
{
    public class GetContactValidatorTests
    {
     

        GetContactValidator _validator;
        GetContactRequest _request;
        
        public GetContactValidatorTests()
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
