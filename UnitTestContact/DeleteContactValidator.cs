using System;
using Xunit;
using Webapi.Contact.Features.Contacts;
using MediatR;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace UnitTestContact
{
    public class DeleteContactValidatorTests
    {


        DeleteValidator _validator;
        DeleteContactRequest _request;

        public DeleteContactValidatorTests()
        {
            _validator = new DeleteValidator();
            _request = new DeleteContactRequest();

        }
        [Theory]
        [InlineData(1.2, true)]
        [InlineData(1, true)]
        [InlineData(100, true)]
        [InlineData(-100, true)]
        public void validateGetContact(int contactId, bool isValid)
        {
            _request.ContactId = contactId;
            _validator.Validate(_request).IsValid.Should().Be(isValid);
        }

    }
}
