using System;
using Xunit;
using Webapi.Contact.Features.Contacts;
using MediatR;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace UnitTestContact
{
    public class UpdateContactTestCaseTests
    {


        UpdateContactValidator _validator;
        UpdateContactRequest _request;

        public UpdateContactTestCaseTests()
        {
            _validator = new UpdateContactValidator();
            _request = new UpdateContactRequest();

        }
        [Theory]
        [InlineData(1,"firsttest1", "lastTEst1", "test@email.com", "12343", true, true)]
        [InlineData(1,"firsttest1", "lastTEst1", "test@email.com", "12343", false, true)]
        [InlineData(1,"firsttest1", "lastTEst1", "test", "12343", true, false)]
        [InlineData(1,"firsttest1", "", "test@email.com", "12343", true, false)]
        [InlineData(1,"firsttest1", "lastTEst1", "test", "", true, false)]
        [InlineData(1,"", "", "test@email.com", "12343", true, false)]
        public void validateGetContact(int contactId,string firstName, string lastName, string email, string phone, bool status, bool isValid)
        {
            _request.ContactId = contactId;
            _request.FirstName = firstName;
            _request.LastName = lastName;
            _request.Email = email;
            _request.PhoneNumber = phone;
            _request.Status = status;
            _validator.Validate(_request).IsValid.Should().Be(isValid);
        }

    }
}
