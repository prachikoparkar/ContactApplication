using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Webapi.Contact.Helpers
{
    public class CustomAuthorize : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
                throw new ArgumentException(null, nameof(context));
            SummaryResponse SummaryResponse = new SummaryResponse
            {
                Status = ResponseStatus.AuthFailure.ToString(),
                Type = ResponseType.F.ToString(),
                Description = new List<string> { ValidationMessages.InvalidAuthToken }
            };

            var user = context.HttpContext.User;
            if (user != null)
            {
                if (!user.Identity.IsAuthenticated)
                {
                    context.Result = new BadRequestObjectResult(SummaryResponse) { StatusCode = 401 };
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //NotImplemented
        }


    }
}
