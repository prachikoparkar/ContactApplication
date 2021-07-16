using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webapi.Contact.Helpers
{
  
    public class SummaryResponse
    {
        public string Status { get; set; }
        public string Type { get; set; }
        public List<string> Description { get; set; }
    }
    public enum ResponseStatus
    {
        Success,
        Failed,
        AuthFailure,
        NotFound,
        BusinessRuleViolation,
        TimeOut
    }
    public enum ResponseType
    {
        S,
        F,
        E
    }
    public static class ValidationMessages
    {
        public static readonly string InvalidDate = "Invalid Date";
        public static readonly string InvalidAuthToken = "Invalid Authorization Token";

    }
}
