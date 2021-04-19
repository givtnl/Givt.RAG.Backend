using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using backend.business.Infrastructure;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NJsonSchema.Annotations;

namespace backend.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            var model = new ExceptionModel
            {
                ErrorCode = (HttpStatusCode.InternalServerError).ToString(),
                ErrorMessage = context.Exception.Message
            };
            if (context.Exception is ValidationException validateException)
            {
                model.ErrorCode = (HttpStatusCode.BadRequest).ToString();
                model.AdditionalInformation = validateException.Errors;
            }
            else
            {
                _logger.LogError($"Unhandled exception occured on {context.HttpContext.Request.GetDisplayUrl()} {context.Exception.Message}");
            }

            context.Result = new JsonResult(model, new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }

    internal class ExceptionModel
    {
        [NotNull]
        public string ErrorCode { get; set; }
        [NotNull]
        public string ErrorMessage { get; set; }
        public List<ValidationExceptionError> AdditionalInformation { get; set; }
    }
}