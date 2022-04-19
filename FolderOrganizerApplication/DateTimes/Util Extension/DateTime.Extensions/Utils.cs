using System;
using MMS.Framework.Util_Extension.Number.Extensions;

namespace MMS.Framework.Util_Extension.DateTime.Extensions
{
    public static class Utils
    {
        /// <summary>
        /// Represents TimeSpan in words
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>String representation of the timespan</returns>
        public static string ToWords(this TimeSpan val)
        {
            return TimeSpanArticulator.Articulate(val, TemporalGroupType.day
                | TemporalGroupType.hour
                | TemporalGroupType.minute
                | TemporalGroupType.month
                | TemporalGroupType.second
                | TemporalGroupType.week
                | TemporalGroupType.year);
        }
        /// <summary>
        /// Converts Datetime value at midnight
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DateTime value with time set to Midnight</returns>
        public static System.DateTime MidNight(this System.DateTime val)
        {
            return new System.DateTime(val.Year, val.Month, val.Day, 0, 0, 0);
        }
        /// <summary>
        /// Converts Datetime value at noon
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DateTime value with time set to Noon</returns>
        public static System.DateTime Noon(this System.DateTime val)
        {
            return new System.DateTime(val.Year, val.Month, val.Day, 12, 0, 0);
        }
        /// <summary>
        /// Checks if the Datetime lies within a given range
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="floor">The floor value of the range</param>
        /// <param name="ceiling">The ceiling value of the range</param>
        /// <param name="includeBase">True to include the floor and ceiling values for comparison</param>
        /// <returns>Returns true if the value lies within the range</returns>
        public static bool IsWithinRange(this System.DateTime val, System.DateTime floor, System.DateTime ceiling, bool includeBase)
        {
            if (floor > ceiling)
                throw new InvalidOperationException("floor value cannot be greater than ceiling value");
            if (floor == ceiling)
                throw new InvalidOperationException("floor value cannot be equal to ceiling value");

            if (includeBase)
                return (val >= floor && val <= ceiling);
            else
                return (val > floor && val < ceiling);
        }
        /// <summary>
        /// Calculates the TimeSpan between the current Datetime and the provided Datetime
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>TimeSpan between the current DateTime & the provided DateTime</returns>
        public static TimeSpan GetTimeSpan(this System.DateTime val)
        {
            TimeSpan dateDiff;
            if (val < System.DateTime.Now)
                dateDiff = System.DateTime.Now.Subtract(val);
            else if (val > System.DateTime.Now)
                dateDiff = val.Subtract(System.DateTime.Now);
            else
                throw new InvalidOperationException("value cannot be equal to DateTime.Now");
            return dateDiff;
        }

        public static int CalculateAge(this System.DateTime? birthDate)
        {
            if (birthDate.HasValue)
            {
                int age = System.DateTime.Now.Year - birthDate.Value.Year;
                if (System.DateTime.Now.Month < birthDate.Value.Month ||
                    (System.DateTime.Now.Month == birthDate.Value.Month && System.DateTime.Now.Day < birthDate.Value.Day)) age--;
                return age;
            }
            throw new InvalidCastException();
        }
        public static int CalculateAge(this System.DateTime birthDate)
        {
            
                int age = System.DateTime.Now.Year - birthDate.Year;
                if (System.DateTime.Now.Month < birthDate.Month ||
                    (System.DateTime.Now.Month == birthDate.Month && System.DateTime.Now.Day < birthDate.Day)) age--;
                return age;
            
        }

        public static bool IsLessThan(this Age age, int _age)
        {
            if (age.Years > _age)
                return false;
            if (age.Years < _age)
                return true;
            if (age.Days > 0 || age.Months > 0)
                return false;
            return true;

        }
        public static Age CalculateAge(this System.DateTime birthDate,System.DateTime FromDateTime)
        {
            var age = birthDate.AgeAt(FromDateTime);
            //int age = DateTime.Now.Year - birthDate.Year;
            //if (DateTime.Now.Month < birthDate.Month ||
            //    (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day)) age--;
            return age;
        }
        public static Age CalculateAge2(this System.DateTime birthDate)
        {
            var age = birthDate.AgeAt(System.DateTime.Now);
            //int age = DateTime.Now.Year - birthDate.Year;
            //if (DateTime.Now.Month < birthDate.Month ||
            //    (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day)) age--;
            return age;
        }

        public static double CalculateAgeExact(this System.DateTime? birthDate)
        {
            if (birthDate.HasValue)
                return System.DateTime.Now.Subtract(birthDate.Value).TotalDays / (365.0);
            throw new InvalidCastException();
        }

        public static TimeSpan CalculateWorkingHour(this System.DateTime? FinishDateTime,System.DateTime? StartDateTime)
        {
            if (!FinishDateTime.HasValue || !StartDateTime.HasValue)
                return new TimeSpan();
            TimeSpan WorkingHour = new TimeSpan();
            
            for (var i = StartDateTime.Value; i < FinishDateTime.Value; i = i.AddMinutes(5))
            {
                if (i.DayOfWeek != DayOfWeek.Friday)
                {
                    if (i.TimeOfDay.Hours >= 8 && i.TimeOfDay.Hours < 17)
                    {
                        WorkingHour= WorkingHour.Add(new TimeSpan(0,5,0));
                    }
                }
            }

            return WorkingHour;
        }

        
    }
}
