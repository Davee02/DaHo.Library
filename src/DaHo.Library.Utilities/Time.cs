using System;

namespace DaHo.Library.Utilities
{
    public struct Time
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }

        public Time(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public override string ToString() => $"{Hours}h {Minutes}m";

        public static Time FromHours(decimal hours)
        {
            var timespan = TimeSpan.FromHours((double)hours);
            return new Time((int)timespan.TotalHours, timespan.Minutes);
        }

        public static Time FromMinutes(decimal minutes)
        {
            var timespan = TimeSpan.FromMinutes((double)minutes);
            return new Time((int)timespan.TotalHours, timespan.Minutes);
        }

        public static Time FromSeconds(decimal seconds)
        {
            var timespan = TimeSpan.FromSeconds((double)seconds);
            return new Time((int)timespan.TotalHours, timespan.Minutes);
        }

        public bool Equals(Time other)
        {
            return this.Hours == other.Hours 
                && this.Minutes == other.Minutes;
        }

        public override bool Equals(object obj)
        {
            if (obj is Time time)
                return Equals(time);

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode
                .Of(Hours)
                .And(Minutes);
        }

        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Time left, Time right)
        {
            return !(left == right);
        }
    }

}
