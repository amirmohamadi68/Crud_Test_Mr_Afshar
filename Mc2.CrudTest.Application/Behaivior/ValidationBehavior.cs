using FluentValidation;
using Mc2.CrudTest.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Behaivior
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            //pre
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var context = new ValidationContext<TRequest>(request);
            if (context == null) throw new ArgumentNullException(nameof(context));
            var validationFails =
                            await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var errors = validationFails.Where(rtesult => !rtesult.IsValid)
                .SelectMany(rtesult => rtesult.Errors)
                .Select(validationFailer =>
                    new ValidationError(validationFailer.PropertyName, validationFailer.ErrorMessage))
                .ToList();
            if (errors.Any())
            {
                //or return GenericResponse<ValidationError>
                throw new CustomerValidateException(errors);
            }

            var response = await next();

            //post

            return response;
        }
    }
}
