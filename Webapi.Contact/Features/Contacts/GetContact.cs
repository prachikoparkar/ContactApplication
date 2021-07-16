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
    public class GetHandler : IRequestHandler<GetContactRequest, GetContactResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetHandler> _logger;
        public readonly ContactsDBContext _dbContext;
        public GetHandler(IConfiguration configuration, ILogger<GetHandler> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _dbContext = new ContactsDBContext();                ;
        }
        public async Task<GetContactResponse> Handle(GetContactRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                using IDbConnection connection = new SqlConnection();
                var result = await _dbContext.Contact.
                                    Where(s => (s.Id == request.contactId))
                                    .FirstOrDefaultAsync().ConfigureAwait(false);

                if (result != null)
                {
                    _logger.LogInformation("Get Contact Details Feetched for ", request);
                    return new GetContactResponse()
                    {
                        SummaryResponse = new SummaryResponse()
                        {
                            Status = ResponseStatus.Success.ToString(),
                            Type = ResponseType.S.ToString(),
                            Description = new List<string>() { "Contacts Fetched Successfully" }
                        },
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        Email = result.Email,
                        PhoneNumber = result.PhoneNumber,
                        Status = result.Status

                    };
                }
                else
                {
                    throw new NotFoundException("Contact Not Found", "ContactRequest", request.ToString(), ResponseStatus.NotFound.ToString(), StatusCodes.Status404NotFound, ResponseType.F.ToString());

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;


            }

        }
    }
    public class GetContactRequest:IRequest<GetContactResponse>
    {
        public int contactId { get; set; }

    }
    public class GetContactResponse
    {
        public SummaryResponse SummaryResponse { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
    public class GetContactValidator:AbstractValidator<GetContactRequest>
    {
        public GetContactValidator()
        {
            RuleFor(p => p.contactId)
                .Cascade(CascadeMode.Stop)             
                .NotEmpty();
            
                    
            
        }
    }
}
