using HolidayOptimization.Application.Exceptions;
using System.Collections.Generic;

namespace HolidayOptimization.Application.Queries.LongestHolidayAroundTheWorld.Extensions
{
    public static class TİmeZoneExtension
    {
        public static int GetTimeZoneUtcMinutesDifference(string timezone)
        {
            if (!TimeZoneUtcMinutesDifference.ContainsKey(timezone))
            {
                throw new ApiException($"Timezone: {timezone} is not supported. Please add it to the supported timezones.");
            }

            return TimeZoneUtcMinutesDifference[timezone];
        }

        private static Dictionary<string, int> TimeZoneUtcMinutesDifference = new Dictionary<string, int>
        {
            {"UTC+13:00", 13 *  60 },
            {"UTC+12:45", 12 *  60 + 45 },
            {"UTC+12:00", 12 *  60 },
            {"UTC+11:30", 11 *  60 + 30},
            {"UTC+11:00", 11 *  60 },
            {"UTC+10:30", 10 *  60 + 30},
            {"UTC+10:00", 10 *  60 },
            {"UTC+09:30", 9 *  60 + 30},
            {"UTC+09:00", 9 *  60 },
            {"UTC+08:30", 8 *  60 + 30},
            {"UTC+08:00", 8 *  60 },
            {"UTC+07:30", 7 *  60 + 30},
            {"UTC+07:00", 7 *  60 },
            {"UTC+06:30", 6 *  60 + 30},
            {"UTC+06:00", 6 *  60 },
            {"UTC+05:30", 5 *  60 + 30},
            {"UTC+05:00", 5 *  60 },
            {"UTC+04:30", 4 *  60 + 30 },
            {"UTC+04:00", 4 *  60 },
            {"UTC+03:30", 3 *  60 + 30},
            {"UTC+03:00", 3 *  60 },
            {"UTC+02:30", 2 *  60 + 30},
            {"UTC+02:00", 2 *  60 },
            {"UTC+01:30", 1 *  60 + 30},
            {"UTC+01:00", 1 *  60 },
            {"UTC+00:30", 0 *  60 + 30},

            {"UTC+00:00", 0},
            {"UTC", 0},

            {"UTC-12:00", -12 *  60 },
            {"UTC-11:30", -11 *  60 - 30},
            {"UTC-11:00", -11 *  60 },
            {"UTC-10:30", -10 *  60 - 30},
            {"UTC-10:00", -10 *  60 },
            {"UTC-09:30", -9 *  60 - 30},
            {"UTC-09:00", -9 *  60 },
            {"UTC-08:30", -8 *  60 - 30},
            {"UTC-08:00", -8 *  60 },
            {"UTC-07:30", -7 *  60 - 30},
            {"UTC-07:00", -7 *  60 },
            {"UTC-06:30", -6 *  60 - 30},
            {"UTC-06:00", -6 *  60 },
            {"UTC-05:30", -5 *  60 - 30},
            {"UTC-05:00", -5 *  60 },
            {"UTC-04:30", -4 *  60 - 30 },
            {"UTC-04:00", -4 *  60 },
            {"UTC-03:30", -3 *  60 - 30},
            {"UTC-03:00", -3 *  60 },
            {"UTC-02:30", -2 *  60 - 30},
            {"UTC-02:00", -2 *  60 },
            {"UTC-01:30", -1 *  60 - 30},
            {"UTC-01:00", -1 *  60 },
            {"UTC-00:30", - 30},
        };
    }
}
