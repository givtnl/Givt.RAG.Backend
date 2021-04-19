using System;
using System.Collections.Generic;

namespace backend.business.Infrastructure
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string> Errors { get; set; } = new();
        public ValidationException(): base("Validation error(s) occured")
        {
            
        }
    }
}