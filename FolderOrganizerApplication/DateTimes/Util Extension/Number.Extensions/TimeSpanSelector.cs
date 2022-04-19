using System;

namespace MMS.Framework.Util_Extension.Number.Extensions
{
    public abstract class TimeSpanSelector
    {
        protected TimeSpan myTimeSpan;

        internal int ReferenceValue
        {
            set { myTimeSpan = MyTimeSpan(value); }
        }

        public System.DateTime Ago { get { return System.DateTime.Now - myTimeSpan; } }
        public System.DateTime FromNow { get { return System.DateTime.Now + myTimeSpan; } }
        public System.DateTime AgoSince(System.DateTime dt) { return dt - myTimeSpan; }
        public System.DateTime From(System.DateTime dt) { return dt + myTimeSpan; }
        protected abstract TimeSpan MyTimeSpan(int refValue);

    }

    internal class WeekSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(7 * refValue, 0, 0, 0);
        }
    }

    internal class DaysSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(refValue, 0, 0, 0);
        }
    }

    internal class YearsSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(365 * refValue, 0, 0, 0);
        }
    }
    internal class HourSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(0, refValue, 0, 0);
        }
    }
    internal class MinuteSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(0, 0, refValue, 0);
        }
    }
    internal class SecondSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(0, 0, 0,refValue);
        }
    }
}
