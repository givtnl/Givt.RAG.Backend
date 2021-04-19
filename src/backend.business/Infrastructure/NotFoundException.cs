using System;

namespace backend.business.Infrastructure
{
    public class NotFoundException : Exception
    {
        public NotFoundException(): base("Resource not found")
        {
            
        }
    }
}