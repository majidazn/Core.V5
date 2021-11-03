using FluentValidation;
using Framework.Api;
using Framework.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;
        public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .Distinct()
                .ToList();

            if (failures.Any())
            {
                // throw new OrderingDomainException(
                //   $"Command Validation Errors for type {typeof(TRequest).Name}", new ValidationException("Validation exception", failures));

                var message = string.Join(" | ", failures);
                throw new AppException(ApiResultStatusCode.BadRequest, message);
            }

            var response = await next();
            return response;
        }
    }
}
