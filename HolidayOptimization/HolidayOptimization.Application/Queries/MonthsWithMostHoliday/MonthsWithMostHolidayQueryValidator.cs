using FluentValidation;
using HolidayOptimization.Application.Extensions;

namespace HolidayOptimization.Application.Queries.MonthsWithMostHoliday
{
    public class MonthsWithMostHolidayQueryValidator : AbstractValidator<MonthsWithMostHolidayQuery>
    {
        public MonthsWithMostHolidayQueryValidator()
        {
            RuleFor(c => c.Year)
                .Must(IsValid)
                .WithMessage("{PropertyName} must be between " + DateTimeYearExtension.StartYear + " and " + DateTimeYearExtension.EndYear + ".");
        }

        private static bool IsValid(int year)
        {
            return DateTimeYearExtension.IsYearValid(year);
        }
    }
}
