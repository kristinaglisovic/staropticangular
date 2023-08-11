using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Api.Extensions
{
    public static class ValidationExtensions
    {
        public static UnprocessableEntityObjectResult ToUnprocessableEntity(this IEnumerable<ValidationFailure> errors)
        {

            var errorObj = new {
                errors=errors.Select(x => new
                {
                    x.ErrorMessage,
                    x.PropertyName
                })
            };

            return new UnprocessableEntityObjectResult(errorObj);
        }
        
    }
}
