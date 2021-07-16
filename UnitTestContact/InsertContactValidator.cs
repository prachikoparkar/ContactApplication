using System;
using Xunit;
using Webapi.Contact.Features.Contacts;
using MediatR;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace UnitTestContact
{
    public class InsertContactTestCaseTests
    {


        InsertContactValidator _validator;
        InsertContactRequest _request;

        public InsertContactTestCaseTests()
        {
            _validator = new InsertContactValidator();
            _request = new InsertContactRequest();

        }
        [Theory]
        [InlineData("firsttest1","lastTEst1", "test@email.com","12343",true , true)]
        [InlineData("firsttest1", "lastTEst1", "test@email.com", "12343", false, true)]
        [InlineData("firsttest1", "lastTEst1", "test", "12343", true, false)]
        [InlineData("firsttest1", "", "test@email.com", "12343", true, false)]
        [InlineData("firsttest1", "lastTEst1", "test", "", true, false)]
        [InlineData("", "", "test@email.com", "12343", true, false)]
        public void validateGetContact(string firstName,string lastName,string email,string phone,bool status, bool isValid)
        {
            _request.FirstName = firstName;
            _request.LastName = lastName;
            _request.Email = email;
            _request.PhoneNumber     = phone;
            _request.Status = status;
            _validator.Validate(_request).IsValid.Should().Be(isValid);
        }

    }
}
