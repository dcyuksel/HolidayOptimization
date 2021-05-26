using System;

namespace HolidayOptimization.Application.Exceptions
{
    public class ApiException : Exception
    {

        public ApiException() : base() { }

        public ApiException(string message) : base(message)
        {
        }
    }
}
