namespace HolidayOptimization.Application.Extensions
{
    public static class DateTimeYearExtension
    {
        public static readonly int StartYear = 1;
        public static readonly int EndYear = 9999;

        public static bool IsYearValid(int year)
        {
            if (year > EndYear || year < StartYear) return false;

            return true;
        }
    }
}
