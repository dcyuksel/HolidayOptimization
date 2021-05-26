using HolidayOptimization.Application.Exceptions;
using HolidayOptimization.Application.Queries.LongestHolidayAroundTheWorld.Extensions;
using NUnit.Framework;

namespace HolidayOptimization.Application.UnitTests.Queries.LongestHolidayAroundTheWorld.Extensions
{
    public class TİmeZoneExtensionUnitTests
    {
        [Test]
        [TestCase("UTC+13:00", 13 * 60)]
        [TestCase("UTC", 0)]
        [TestCase("UTC+00:00", 0)]
        [TestCase("UTC-00:30", -30)]
        [TestCase("UTC-04:30", -270)]
        public void GetTimeZoneUtcMinutesDifferenceTest(string timeZone, int expectedResult)
        {
            var result = TİmeZoneExtension.GetTimeZoneUtcMinutesDifference(timeZone);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("UTC-24:30")]
        public void GetTimeZoneUtcMinutesDifferenceWithUnsupportedTimeZoneTest(string timeZone)
        {
            var ex = Assert.Throws<ApiException>(() => TİmeZoneExtension.GetTimeZoneUtcMinutesDifference(timeZone));
            Assert.That(ex.Message, Is.EqualTo("Timezone: UTC-24:30 is not supported. Please add it to the supported timezones."));
        }
    }
}
