using System;
using System.Collections.Generic;
using System.Linq;

namespace DaHo.Library.Utilities
{
    public static class TimeSpanUtilities
    {
        public static IEnumerable<TimeSpan> GetEveryXMinutesForDay(int minutes)
        {
            var startTime = TimeSpan.Zero;
            foreach (var quarterHourOffset in Enumerable.Range(0, 24 * 60 / minutes))
            {
                yield return startTime.Add(TimeSpan.FromMinutes(quarterHourOffset * minutes));
            }
        }

        public static TimeSpan RoundToNearest(this TimeSpan a, TimeSpan roundTo)
        {
            long ticks = (long)(Math.Round(a.Ticks / (double)roundTo.Ticks) * roundTo.Ticks);
            return new TimeSpan(ticks);
        }

        public static bool IsFullHour(this TimeSpan a) => a.Minutes == 0 && a.Seconds == 0;
    }
}
