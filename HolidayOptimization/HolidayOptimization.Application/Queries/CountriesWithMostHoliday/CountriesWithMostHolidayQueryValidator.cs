using FluentValidation;
using HolidayOptimization.Application.Extensions;

namespace HolidayOptimization.Application.Queries.CountriesWithMostHoliday
{
    public class CountriesWithMostHolidayQueryValidator : AbstractValidator<CountriesWithMostHolidayQuery>
    {
        public CountriesWithMostHolidayQueryValidator()
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
