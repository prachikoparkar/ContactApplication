using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace Webapi.Contact.Helpers
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task Invoke(HttpContext context)
        {

        }
    }
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException(string _exceptionMessage, string _fieldName, string _fieldValue, string _statusMessage, int _statusCode, string _type)
        {
            Message = _exceptionMessage;
            FieldName = _fieldName;
            FieldValue = _fieldValue;
            StatusCode = _statusCode;
            StatusMessage = _statusMessage;
            Type = _type;

        }
        public override string Message { get; }
        public string FieldName { get; }
        public string FieldValue { get; }
        public string StatusMessage { get; }
        public int StatusCode { get; }
        public string Type { get; }
    }
}
