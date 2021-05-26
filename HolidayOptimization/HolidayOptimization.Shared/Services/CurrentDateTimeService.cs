using HolidayOptimization.Application.Interfaces.Shared;
using System;

namespace HolidayOptimization.Shared.Services
{
    public class CurrentDateTimeService : ICurrentDateTimeService
    {
        public int GetCurrentYear()
        {
            return DateTime.Now.Year;
        }
    }
}
