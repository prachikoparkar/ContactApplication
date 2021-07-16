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
    public class DeleteContactHandler : IRequestHandler<DeleteContactRequest, DeleteContactResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DeleteContactHandler> _logger;
        public readonly ContactsDBContext _dbContext;
        public DeleteContactHandler(IConfiguration configuration, ILogger<DeleteContactHandler> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _dbContext = new ContactsDBContext(); ;
        }
        public async Task<DeleteContactResponse> Handle(DeleteContactRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));
                using IDbConnection connection = new SqlConnection();

                var _contact = _dbContext.Contact.
                                    Where(s => (s.Id == request.ContactId))
                                    .First();
                _contact.Status = false;

                var result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);

                if (result > 0)
                {
                    _logger.LogInformation("Contact  deleted: ", request);
                    return new DeleteContactResponse()
                    {
                        SummaryResponse = new SummaryResponse()
                        {
                            Status = ResponseStatus.Success.ToString(),
                            Type = ResponseType.S.ToString(),
                            Description = new List<string>() { "Contact deleted Successfully" }
                        },


                    };
                }
                else
                {
                    return new DeleteContactResponse()
                    {
                        SummaryResponse = new SummaryResponse()
                        {
                            Status = ResponseStatus.Failed.ToString(),
                            Type = ResponseType.F.ToString(),
                            Description = new List<string>() { "Error Occured while deleting contact" }
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
    public class DeleteContactRequest : IRequest<DeleteContactResponse>
    {

        public int ContactId { get; set; }

    }
    public class DeleteContactResponse
    {
        public SummaryResponse SummaryResponse { get; set; }

    }
    public class DeleteValidator : AbstractValidator<DeleteContactRequest>
    {
        public DeleteValidator()
        {
            RuleFor(p => p.ContactId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();



        }
    }
}


