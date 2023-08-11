using System;
using Blog.Application.UseCases;
using FluentValidation;

namespace Blog.Implementation.Validators
{
    public class SearchUseCaseLogsValidator : AbstractValidator<UseCaseLogSearch>
    {
        public SearchUseCaseLogsValidator()
        {
            RuleFor(x => x.DateFrom).Cascade(CascadeMode.Stop).NotEmpty()
                .WithMessage("Date from is required.")
                .Must(DateDiffLessThan30Days).WithMessage("Date difference must be less than 30 days.");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Date to is required.");


        }

        private bool DateDiffLessThan30Days(UseCaseLogSearch search, DateTime? dateFrom)
        {
            if (!search.DateTo.HasValue)
            {
                return false;
            }

            var timeSpan = (search.DateTo - search.DateFrom).Value;

            //menjamo opseg dana
            return timeSpan.TotalDays <= 30;
        }
    }
}
