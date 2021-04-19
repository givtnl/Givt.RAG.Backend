using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace backend.business.Infrastructure
{
    public class HackatonPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _businessValidators;

        public HackatonPipelineBehavior(IEnumerable<IValidator<TRequest>> businessValidators)
        {
            _businessValidators = businessValidators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _businessValidators.Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (!failures.Any())
                return await next();

            var toThrowException = new ValidationException
            {
                Errors = failures
                    .GroupBy(x => x.PropertyName)
                    .Select(x => new ValidationExceptionError{ErrorMessage = x.FirstOrDefault()?.ErrorMessage, Property = x.Key}).ToList()
            };

            throw toThrowException;
        }
    }
}