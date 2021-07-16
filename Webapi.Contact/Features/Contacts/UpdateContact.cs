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
    public class UpdateContactHandler : IRequestHandler<UpdateContactRequest, UpdateContactResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UpdateContactHandler> _logger;
        public readonly ContactsDBContext _dbContext;
        public UpdateContactHandler(IConfiguration configuration, ILogger<UpdateContactHandler> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _dbContext = new ContactsDBContext(); ;
        }
        public async Task<UpdateContactResponse> Handle(UpdateContactRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));
                using IDbConnection connection = new SqlConnection();

                var _contact = _dbContext.Contact.
                                    Where(s => (s.Id == request.ContactId))
                                    .First();
                _contact.FirstName = request.FirstName;
                _contact.LastName = request.LastName;
                _contact.PhoneNumber = request.PhoneNumber;
                _contact.Email = request.Email;
                _contact.Status = request.Status;

                var result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                if (result > 0)
                {
                    _logger.LogInformation("Contact details updated: ", request);
                    return new UpdateContactResponse()
                    {
                        SummaryResponse = new SummaryResponse()
                        {
                            Status = ResponseStatus.Success.ToString(),
                            Type = ResponseType.S.ToString(),
                            Description = new List<string>() { "Contact updated Successfully" }
                        },


                    };
                }
                else
                {
                    return new UpdateContactResponse()
                    {
                        SummaryResponse = new SummaryResponse()
                        {
                            Status = ResponseStatus.Failed.ToString(),
                            Type = ResponseType.F.ToString(),
                            Description = new List<string>() { "Error Occured while updating contact" }
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
    public class UpdateContactRequest : IRequest<UpdateContactResponse>
    {

        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }

    }
    public class UpdateContactResponse
    {
        public SummaryResponse SummaryResponse { get; set; }

    }
    public class UpdateContactValidator : AbstractValidator<UpdateContactRequest>
    {
        public UpdateContactValidator()
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
            .Must(x => x == false || x == true)
            .NotEmpty();
        }
    }
}