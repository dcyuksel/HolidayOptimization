using HolidayOptimization.Application.Extensions;
using NUnit.Framework;

namespace HolidayOptimization.Application.UnitTests.Extensions
{
    public class DateTimeYearExtensionUnitTests
    {
        [Test]
        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(2021, true)]
        [TestCase(9999, true)]
        [TestCase(10000, false)]
        public void IsYearValidTest(int year, bool expectedResult)
        {
            var result = DateTimeYearExtension.IsYearValid(year);
            Assert.AreEqual(result, expectedResult);
        }
    }
}
