using System;
using System.Collections.Generic;
using NJsonSchema.Annotations;

namespace backend.business.Infrastructure
{
    public class ValidationException : Exception
    {
        public List<ValidationExceptionError> Errors { get; set; } = new();
        public ValidationException(): base("Validation error(s) occured")
        {
            
        }
    }

    public class ValidationExceptionError
    {
        [NotNull]
        public string Property { get; set; }
        [NotNull]
        public string ErrorMessage { get; set; }
    }
}