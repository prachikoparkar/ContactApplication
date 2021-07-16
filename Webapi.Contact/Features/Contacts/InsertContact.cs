using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Webapi.Contact.Models;
using FluentValidation;
using Webapi.Contact.Helpers;
using System.Threading;

namespace Webapi.Contact.Features.Contacts
{
    public class InsertContactHandler : IRequestHandler<InsertContactRequest, InsertContactResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<InsertContactHandler> _logger;
        public readonly ContactsDBContext _dbContext;
        public InsertContactHandler(IConfiguration configuration, ILogger<InsertContactHandler> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _dbContext = new ContactsDBContext(); ;
        }
        public async Task<InsertContactResponse> Handle(InsertContactRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));


                var _contact = new Webapi.Contact.Models.Contact
                {
                    FirstName = request.FirstName.Trim(),
                    LastName = request.LastName.Trim(),
                    Email = request.Email.Trim(),
                    PhoneNumber = request.PhoneNumber.Trim(),
                    Status = request.Status,


                };
                using IDbConnection connection = new SqlConnection();
                _dbContext.Contact.Add(_contact);

                var result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                if (result == 1)
                {
                    _logger.LogInformation("Contact details Inserted: ", request);
                    return new InsertContactResponse()
                    {
                        SummaryResponse = new SummaryResponse()
                        {
                            Status = ResponseStatus.Success.ToString(),
                            Type = ResponseType.S.ToString(),
                            Description = new List<string>() { "Contact Inserted Successfully" }
                        },


                    };
                }
                else
                {
                    return new InsertContactResponse()
                    {
                        SummaryResponse = new SummaryResponse()
                        {
                            Status = ResponseStatus.Failed.ToString(),
                            Type = ResponseType.F.ToString(),
                            Description = new List<string>() { "Error Occured while Inserting contact" }
                        },


                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

    }
    public class InsertContactRequest : IRequest<InsertContactResponse>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }

    }
    public class InsertContactResponse
    {
        public SummaryResponse SummaryResponse { get; set; }

    }
    public class InsertContactValidator : AbstractValidator<InsertContactRequest>
    {
        public InsertContactValidator()
        {
            RuleFor(p => p.FirstName)
                .Cascade(CascadeMode.Stop)
                .Matches("^[0-9A-Za-z-_.'-@\\/+~$*;!]+$")
                .NotEmpty();

            RuleFor(p => p.LastName)
              .Cascade(CascadeMode.Stop)
              .Matches("^[0-9A-Za-z-_.'-@\\/+~$*;!]+$")
              .NotEmpty();
            RuleFor(p => p.Email)
             .Cascade(CascadeMode.Stop)
             .Matches("[A-Z0-9a-z._%+-]+@ [A-Za-z0-9.-]+\\. [A-Za-z] {2,64}")
             .NotEmpty();

            RuleFor(p => p.Status)
            .Cascade(CascadeMode.Stop)
            .Must(x=> x==false || x==true)
            .NotEmpty();
        }
    }
}
