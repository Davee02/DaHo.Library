using NUnit.Framework;
using System;
using System.Linq;

namespace DaHo.Library.Utilities.Test
{
    [TestFixture]
    class TimeSpanUtilities_Tests
    {
        [Test]
        [TestCase(10, 10, 10, false)]
        [TestCase(10, 0, 10, false)]
        [TestCase(10, 10, 0, false)]
        [TestCase(0, 0, 10, false)]
        [TestCase(10, 0, 0, true)]
        public void IsFullHour_Checks_If_All_Except_Hour_Is_Zero(int hours, int minutes, int seconds, bool expected)
        {
            var timeSpanValue = new TimeSpan(hours, minutes, seconds);
            var actual = timeSpanValue.IsFullHour();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetEveryXMinutesForDay_Test()
        {
            const int xMinutes = 10;
            const int expectedValuesCount = 24 * 60 / xMinutes;

            var values = TimeSpanUtilities.GetEveryXMinutesForDay(xMinutes);

            Assert.AreEqual(expectedValuesCount, values.Count());
        }
    }
}
