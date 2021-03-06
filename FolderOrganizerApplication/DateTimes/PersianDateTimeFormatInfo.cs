using System.Globalization;
using System.Text.RegularExpressions;

namespace FolderOrganizerApplication.DateTimes
{
    [Serializable]
    public class PersianDateTime
    {
        public enum DateTimeFormat
        {
            MmdDhhmmss,
            Hhmmss,
            Mmdd
        }

        private static readonly PersianCalendar Pcal = new PersianCalendar();

        public static DayOfWeek FirstDayOfWeek => DayOfWeek.Saturday;

        public static PersianDateTime Now => MiladiToPersian(DateTime.Now);

        public static PersianDateTime Today => MiladiToPersian(DateTime.Today);

        public PersianDateTime Date()
        {
            return new PersianDateTime(Year, Month, Day);
        }

        public static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "امرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static string GetAbbreviatedMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "فر";
                case 2:
                    return "ار";
                case 3:
                    return "خر";
                case 4:
                    return "تی";
                case 5:
                    return "مر";
                case 6:
                    return "شه";
                case 7:
                    return "مه";
                case 8:
                    return "آب";
                case 9:
                    return "آذ";
                case 10:
                    return "دی";
                case 11:
                    return "به";
                case 12:
                    return "اس";
                default:
                    throw new ArgumentOutOfRangeException(nameof(month));
            }
        }

        private static string GetDayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Saturday:
                    return "شنبه";
                case DayOfWeek.Sunday:
                    return "یکشنبه";
                case DayOfWeek.Monday:
                    return "دوشنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهارشنبه";
                case DayOfWeek.Thursday:
                    return "پنجشنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static string GetAbbreviatedDayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Saturday:
                    return "ش";
                case DayOfWeek.Sunday:
                    return "ی";
                case DayOfWeek.Monday:
                    return "د";
                case DayOfWeek.Tuesday:
                    return "س";
                case DayOfWeek.Wednesday:
                    return "چ";
                case DayOfWeek.Thursday:
                    return "پ";
                case DayOfWeek.Friday:
                    return "ج";
                default:
                    throw new ArgumentOutOfRangeException(nameof(day));
            }
        }

        public string LongDateTimeString
        {
            get
            {
                var persianDt = "{0} {1} {2} {3}  {4}:{5}:{6}";
                persianDt = string.Format(persianDt, GetDayName(GetDayOfWeek),
                    PDay, GetMonthName(Month), PYear, PHour.PadLeft(2, '0'), PMinute.PadLeft(2, '0'),
                    PSecond.PadLeft(2, '0'));
                return persianDt;
            }
        }

        public string LongDateString
        {
            get
            {
                var persianDt = "{0} {1} {2} {3}";
                persianDt = string.Format(persianDt, GetDayName(GetDayOfWeek), PDay, GetMonthName(Month), PYear);
                return persianDt;
            }
        }


        public string ShortDateTimeString
        {
            get
            {
                var persianDt = "{3}:{4} {0}/{1}/{2}";

                persianDt = string.Format(persianDt, PYear, PMonth, PDay, PHour, PMinute);
                return persianDt;
            }

        }

        public string ToLongDateString(char spliter)
        {
            var persianDt = "{0}{4}{1}{4}{2}{4}{3}";
            persianDt = string.Format(persianDt, GetDayName(GetDayOfWeek), PDay, GetMonthName(Month), PYear, spliter);
            return persianDt;
        }

        public string ToLongDateTimeString(char spliter)
        {
            var persianDt = "{0}{4}{1}{4}{2}{4}{3}{4}{5}{4}{6}";
            persianDt = string.Format(persianDt, GetDayName(GetDayOfWeek), PDay, GetMonthName(Month), PYear, spliter,
                PHour, PMinute);
            return persianDt;
        }

        public string ShortDateString
        {
            get
            {
                var persianDt = "{0}/{1}/{2}";

                persianDt = string.Format(persianDt, PYear, PMonth, PDay);
                return persianDt;
            }
        }

        public string ToString(string Spliter)
        {
            var persianDt = "{0}{1}{2}{3}{4}";

            persianDt = string.Format(persianDt, PYear, Spliter, PMonth, Spliter, PDay);
            return persianDt;
        }

        public override string ToString()
        {
            var persianDt = "{0}{1}{2}";

            persianDt = string.Format(persianDt, PYear, PMonth, PDay);
            return persianDt;
        }

        public string PaddedString
        {
            get
            {
                var persianDt = "{0}{1}{2}";

                persianDt = string.Format(persianDt, PYear.PadLeft(4, '0'), PMonth.PadLeft(2, '0'),
                    PDay.PadLeft(2, '0'));
                return persianDt;
            }
        }

        public string LongTimeString
        {
            get
            {
                var PersianDT = "{0}:{1}:{2}";

                PersianDT = string.Format(PersianDT, PHour, PMinute, second);
                return PersianDT;
            }
        }

        public string YearMonthString
        {
            get
            {
                var PersianDT = "{0}{1}";
                PersianDT = string.Format(PersianDT, PYear.PadLeft(4, '0'), PMonth.PadLeft(2, '0'));
                return PersianDT;
            }
        }

        public string ToYearMonthString(char spliter)
        {
            var PersianDT = "{0}{1}{2}";
            PersianDT = string.Format(PersianDT, PYear.PadLeft(4, '0'), spliter, PMonth.PadLeft(2, '0'));
            return PersianDT;
        }

        public string ToLongTimeString(string Splitter)
        {
            var PersianDT = "{0}{3}{1}{3}{2}";

            PersianDT = string.Format(PersianDT, PHour, PMinute, second, Splitter);
            return PersianDT;
        }

        public string ToShortTimeString
        {
            get
            {
                var PersianDT = " {0}:{1}";

                PersianDT = string.Format(PersianDT, PHour, PMinute);
                return PersianDT;
            }
        }

        private DayOfWeek GetDayOfWeek => PersianToMiladi(this).DayOfWeek;

        public static string GetShortPersianDateString(DateTime christDateTime)
        {
            var calendar = new PersianCalendar();

            var Month = calendar.GetMonth(christDateTime).ToString();
            Month = Month.Length == 1 ? "0" + Month : Month;

            var Day = calendar.GetDayOfMonth(christDateTime).ToString();
            Day = Day.Length == 1 ? "0" + Day : Day;

            return $@"{calendar.GetYear(christDateTime)}/{Month}/{Day}";
        }

        public static string GetLongPersianDateString(DateTime christDateTime)
        {
            var calendar = new PersianCalendar();
            return string.Format(@"{0} {1} {2}", calendar.GetYear(christDateTime),
                GetMonthName(calendar.GetMonth(christDateTime)),
                calendar.GetDayOfMonth(christDateTime));
        }

        public static string GetShortPersianDateStringReverse(DateTime christDateTime)
        {
            var calendar = new PersianCalendar();
            var Month = calendar.GetMonth(christDateTime).ToString();
            Month = Month.Length == 1 ? "0" + Month : Month;

            var Day = calendar.GetDayOfMonth(christDateTime).ToString();
            Day = Day.Length == 1 ? "0" + Month : Day;

            return string.Format(@"{2}/{1}/{0}", calendar.GetYear(christDateTime),
                Month, Day);
        }

        public static string GetLongPersianDateStringReverse(DateTime christDateTime)
        {
            var calendar = new PersianCalendar();
            return string.Format(@"{2} {1} {0}", calendar.GetYear(christDateTime),
                GetMonthName(calendar.GetMonth(christDateTime)),
                calendar.GetDayOfMonth(christDateTime));
        }

        private static void CheckDateRange(int month, int day)
        {
            if (month > 12 || month < 1 || day > 31 || day < 1 || month > 6 && day > 30)
                throw new ApplicationException("Not a valid Hijri Shamsi Sate string");
        }

        public static DateTime PersianToMiladi(string persianDate)
        {
            if (!persianDate.Contains("/"))
            {
                var Year = persianDate.Substring(0, 4);
                var Month = persianDate.Substring(4, 2);
                var Day = persianDate.Substring(6, 2);
                return p_PersianToMiladi($"{Year}/{Month}/{Day}");
            }

            return p_PersianToMiladi(persianDate);
        }



        public static DateTime? TryPersianToMiladi(string persianDate)
        {
            try
            {
                if (!string.IsNullOrEmpty( persianDate))
                    return PersianToMiladi(persianDate);
                else
                    return null;
                
            }
            catch
            {
                return null;
            }
        }

        public static DateTime? TryPersianToMiladi(PersianDateTime persianDate)
        {
            try
            {
                return persianDate?.ToMiladi();
            }
            catch
            {
                return null;
            }
        }

        private static DateTime p_PersianToMiladi(string persianDate, string PersianTime = null)
        {
            var re = new Regex(@"(\d{2,4})/(\d{1,2})/(\d{1,2})");
            var revRegex = new Regex(@"(\d{1,2})/(\d{1,2})/(\d{2,4})");
            Match m;
            var Year = 0;
            var Month = 0;
            var Day = 0;
            if (re.IsMatch(persianDate))
            {
                m = re.Match(persianDate);
                Year = int.Parse(m.Groups[1].Value);
                Month = int.Parse(m.Groups[2].Value);
                Day = int.Parse(m.Groups[3].Value);
            }
            else if (revRegex.IsMatch(persianDate))
            {
                m = revRegex.Match(persianDate);
                Year = int.Parse(m.Groups[3].Value);
                Month = int.Parse(m.Groups[2].Value);
                Day = int.Parse(m.Groups[1].Value);
            }

            CheckDateRange(Month, Day);

            var arrPersianTime = new[] { "0", "0" };
            if (!string.IsNullOrEmpty(PersianTime))
                arrPersianTime = PersianTime.Split(':');

            var calendar = new PersianCalendar();
            return calendar.ToDateTime(Year, Month, Day, Convert.ToInt32(arrPersianTime[0]),
                Convert.ToInt32(arrPersianTime[1]), 0, 0);
        }

        public static DateTime PersianToMiladi(PersianDateTime persiandate)
        {
            return Pcal.ToDateTime(persiandate.Year, persiandate.Month, persiandate.Day, persiandate.Hour,
                persiandate.Minute, persiandate.Second, 0);
        }

        public static PersianDateTime MiladiToPersian(string miladiDt)
        {
            var Year = Convert.ToInt32(miladiDt.Substring(0, 4));
            var Month = Convert.ToInt32(miladiDt.Substring(4, 2));
            var Day = Convert.ToInt32(miladiDt.Substring(6, 2));
            var pdate = new DateTime(Year, Month, Day, 0, 0, 0);
            return MiladiToPersian(pdate);
        }

        public static PersianDateTime MiladiToPersian(DateTime miladiDt)
        {
            var year = Pcal.GetYear(miladiDt);
            var month = Pcal.GetMonth(miladiDt);
            var day = Pcal.GetDayOfMonth(miladiDt);
            var hour = Pcal.GetHour(miladiDt);
            var minute = Pcal.GetMinute(miladiDt);
            var second = Pcal.GetSecond(miladiDt);

            var pdate = new PersianDateTime(year, month, day, hour, minute, second);
            return pdate;
        }

        public static string GetPersainDateTimeToLargeString(DateTime dt, string dateTimeFormat)
        {
            var persianDt = "";
            try
            {
                persianDt = GetDayName(dt.DayOfWeek);
                persianDt += " " + Pcal.GetDayOfMonth(dt);
                persianDt += " " + GetMonthName(Pcal.GetMonth(dt))
                                 + " " + Pcal.GetYear(dt);
            }
            catch
            {
            }

            return persianDt;
        }

        public static string GetPersainDateToString(DateTime dt, string dateTimeFormat)
        {
            var persianDt = "";
            try
            {
                persianDt += " " + Pcal.GetDayOfMonth(dt);
                persianDt += " " + GetMonthName(Pcal.GetMonth(dt))
                                 + " " + Pcal.GetYear(dt);
                persianDt += "  " + dt.Hour + ":" + dt.Minute;
            }
            catch
            {
            }

            return persianDt;
        }

        public static string GetPersainDateTimeToShortDateString(DateTime dt, string dateTimeFormat)
        {
            var persianDt = "";
            try
            {
                persianDt = Pcal.GetYear(dt) + "/" + Pcal.GetMonth(dt) + "/" + Pcal.GetDayOfMonth(dt);
            }
            catch
            {
            }

            return persianDt;
        }

        public static string MiladiToGhamari(DateTime dt)
        {
            return $"{dt.Year} , {dt.Day} {GetMonthNameGhamari(dt.Month)} , {GetDayNameGhamari(dt.DayOfWeek)}";
        }

        private static string GetMonthNameGhamari(int month)
        {
            switch (month)
            {
                case 1:
                    return "ینایر";
                case 2:
                    return "فبرایر";
                case 3:
                    return "مارس";
                case 4:
                    return "أبریل";
                case 5:
                    return "مایو";
                case 6:
                    return "یونيو";
                case 7:
                    return "یوليو";
                case 8:
                    return "أغسطس";
                case 9:
                    return "سبتمبر";
                case 10:
                    return "أآتوبر";
                case 11:
                    return "نوفمبر";
                case 12:
                    return "دیسمبر";
                default:
                    throw new ArgumentOutOfRangeException(nameof(month));
            }
        }

        private static string GetDayNameGhamari(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Saturday:
                    return "السبت";
                case DayOfWeek.Sunday:
                    return "الأحد";
                case DayOfWeek.Monday:
                    return "الاثنين";
                case DayOfWeek.Tuesday:
                    return "الثلاثاء";
                case DayOfWeek.Wednesday:
                    return "الأربعاء";
                case DayOfWeek.Thursday:
                    return "الخميس";
                case DayOfWeek.Friday:
                    return "الجمعة";
                default:
                    throw new ArgumentOutOfRangeException(nameof(day));
            }
        }

        public TimeSpan Subtract(PersianDateTime inputDate)
        {
            var dt = PersianToMiladi(this);
            var inputdt = PersianToMiladi(inputDate);

            return dt.Subtract(inputdt);
        }

        public static string GetDateTime(DateTime dt, DateTimeFormat format)
        {
            switch (format)
            {
                case DateTimeFormat.MmdDhhmmss:
                    return dt.ToString("MMddHHmmss");

                case DateTimeFormat.Hhmmss:
                    return dt.ToString("HHmmss");

                case DateTimeFormat.Mmdd:
                    return dt.ToString("MMdd");

                default:
                    throw new ArgumentOutOfRangeException(nameof(format));
            }
        }

        public PersianDateTime AddDays(int i)
        {
            return MiladiToPersian(Pcal.AddDays(PersianToMiladi(this), i));
        }

        public PersianDateTime AddYears(int i)
        {
            return MiladiToPersian(Pcal.AddYears(PersianToMiladi(this), i));
        }

        public PersianDateTime AddMonths(int i)
        {
            return MiladiToPersian(Pcal.AddMonths(PersianToMiladi(this), i));
        }

        public DateTime ToDateTime()
        {
            return PersianToMiladi(this);
        }

        public bool Equals(PersianDateTime other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Day == Day && other.hour == hour && other.minute == minute && other.Year == Year &&
                   other.Month == Month && other.second == second;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(PersianDateTime)) return false;
            return Equals((PersianDateTime)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = Day;
                result = (result * 397) ^ hour;
                result = (result * 397) ^ minute;
                result = (result * 397) ^ Year;
                result = (result * 397) ^ Month;
                result = (result * 397) ^ second;
                return result;
            }
        }

        public DateTime ToMiladi()
        {
            return PersianToMiladi(this);
        }

        #region Constructors

        public PersianDateTime(DateTime miladiDt)
        {
            Year = Pcal.GetYear(miladiDt);
            Month = Pcal.GetMonth(miladiDt);
            Day = Pcal.GetDayOfMonth(miladiDt);
            hour = Pcal.GetHour(miladiDt);
            minute = Pcal.GetMinute(miladiDt);
            second = Pcal.GetSecond(miladiDt);
            week = Pcal.GetWeekOfYear(miladiDt, CalendarWeekRule.FirstDay, FirstDayOfWeek);
        }

        private PersianDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            Year = year;
            Month = month;
            Day = day;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public PersianDateTime(string persianDate)
        {
            if (string.IsNullOrEmpty(persianDate) == false)
            {
                persianDate = persianDate.Replace("/", "");
                persianDate = persianDate.Replace("-", "");

                if (persianDate.Length < 8)
                    persianDate = Now.Year.ToString().Substring(0, 2) + persianDate;

                Year = Convert.ToInt32(persianDate.Substring(0, 4));
                Month = Convert.ToInt32(persianDate.Substring(4, 2));
                Day = Convert.ToInt32(persianDate.Substring(6, 2));

                if (persianDate.Length > 8)
                {
                    hour = Convert.ToInt32(persianDate.Substring(9, 2));
                    minute = Convert.ToInt32(persianDate.Substring(12, 2));
                }
            }
        }

        public PersianDateTime(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public PersianDateTime(int year, int month, int day, int hour, int minute)
        {
            Year = year;
            Month = month;
            Day = day;
            this.hour = hour;
            this.minute = minute;
        }

        #endregion

        #region Public Property

        public int Year
        {
            get;
            //set { year = value; }
        }

        public int Month
        {
            get;
            //set { month = value; }
        }

        public int Day
        {
            get;
            //set { day = value; }
        }

        public int Hour
        {
            get => hour;
            set => hour = value;
        }

        public int Minute
        {
            get => minute;
            set => minute = value;
        }

        public int Second
        {
            get => second;
            set => second = value;
        }

        //public int WeekOfYear
        //{
        //    get => week;
        //    set => week = value;
        //}

        #endregion

        #region Properties

        private int hour;
        private int minute;

        private int second;
        private int week;

        private string PYear => Year.ToString();

        private string PMonth
        {
            get
            {
                if (Month.ToString().Length > 1)
                    return Month.ToString();
                return "0" + Month;
            }
        }

        private string PDay
        {
            get
            {
                if (Day.ToString().Length > 1)
                    return Day.ToString();
                return "0" + Day;
            }
        }

        private string PHour
        {
            get
            {
                if (hour.ToString().Length > 1)
                    return hour.ToString();
                return "0" + hour;
            }
        }

        private string PMinute
        {
            get
            {
                if (minute.ToString().Length > 1)
                    return minute.ToString();
                return "0" + minute;
            }
        }

        private string PSecond
        {
            get
            {
                if (minute.ToString().Length > 1)
                    return second.ToString();
                return "0" + second;
            }
        }

        public PersianDateTime FirstDayOfMonth()
        {
            var dayOfMonth = this.Day;
            return this.AddDays(-dayOfMonth + 1);

        }

        public PersianDateTime FirstDayOfMYear()
        {
            return new PersianDateTime(this.Year+"/01/01");

        }
        public PersianDateTime LastDayOfMonth()
        {
            var dayOfMonth = this.Day;
            return this.AddDays(-dayOfMonth + 1).AddMonths(1).AddDays(-1);

        }
        #endregion



        #region Operators

        public PersianDateTime SetTime(string p)
        {
            var Times = p.Split(':');
            if (Times.Length > 0)
                Hour = Convert.ToInt32(Times[0]);

            if (Times.Length > 1)
                Minute = Convert.ToInt32(Times[1]);

            if (Times.Length > 2)
                Second = Convert.ToInt32(Times[2]);

            return this;
        }

        public static PersianDateTime operator +(PersianDateTime d1, int DayCount)
        {
            if (d1 != null)
            {
                return d1.AddDays(DayCount);
            }
            throw new InvalidOperationException(nameof(d1));
        }

        public static PersianDateTime operator +(PersianDateTime d1, TimeSpan TimeSpace)
        {
            if (d1 != null)
            {
                var md1 = PersianToMiladi(d1);
                md1 = md1.Add(TimeSpace);
                return MiladiToPersian(md1);
            }
            throw new InvalidOperationException(nameof(d1));
        }

        public static PersianDateTime operator -(PersianDateTime d1, int DayCount)
        {
            if (d1 != null)
            {
                return d1.AddDays(-DayCount);
            }
            throw new InvalidOperationException(nameof(d1));
        }

        public static PersianDateTime operator -(PersianDateTime d1, TimeSpan TimeSpace)
        {
            if (d1 != null)
            {
                var md1 = PersianToMiladi(d1);
                md1 = md1.Subtract(TimeSpace);
                return MiladiToPersian(md1);
            }
            throw new InvalidOperationException(nameof(d1));
        }


        public static bool operator >(PersianDateTime d1, PersianDateTime d2)
        {
            DateTime md1 = DateTime.MinValue;
            DateTime md2 = DateTime.MinValue;

            if (d1 != null)
                md1 = PersianToMiladi(d1);

            if (d2 != null)
                md2 = PersianToMiladi(d2);

            return md1.Date > md2.Date;
        }

        public static bool operator <(PersianDateTime d1, PersianDateTime d2)
        {
            DateTime md1 = DateTime.MinValue;
            DateTime md2 = DateTime.MinValue;

            if (d1 != null)
                md1 = PersianToMiladi(d1);

            if (d2 != null)
                md2 = PersianToMiladi(d2);
            return md1.Date < md2.Date;
        }

        public static bool operator >=(PersianDateTime d1, PersianDateTime d2)
        {
            DateTime md1 = DateTime.MinValue;
            DateTime md2 = DateTime.MinValue;

            if (d1 != null)
                md1 = PersianToMiladi(d1);
            if (d2 != null)
                md2 = PersianToMiladi(d2);
            return md1.Date >= md2.Date;
        }

        public static bool operator <=(PersianDateTime d1, PersianDateTime d2)
        {
            DateTime md1 = DateTime.MinValue;
            DateTime md2 = DateTime.MinValue;
            if (d1 != null)
                md1 = PersianToMiladi(d1);

            if (d2 != null)
                md2 = PersianToMiladi(d2);

            return md1.Date <= md2.Date;
        }

        public static bool operator ==(PersianDateTime d1, PersianDateTime d2)
        {
            DateTime? md1 = null, md2 = null;
            if (d1 != null)
                md1 = PersianToMiladi(d1);
            if (d2 != null)
                md2 = PersianToMiladi(d2);
            return md1 == md2;
        }

        public static bool operator !=(PersianDateTime d1, PersianDateTime d2)
        {
            if (ReferenceEquals(d1, null) && ReferenceEquals(d2, null))
                return false;

            if (ReferenceEquals(d1, null) || ReferenceEquals(d2, null))
                return true;

            var md1 = PersianToMiladi(d1);
            var md2 = PersianToMiladi(d2);
            return md1 != md2;
        }

        public static PersianDateTime operator ++(PersianDateTime d1)
        {
            var md1 = PersianToMiladi(d1);
            return MiladiToPersian(md1.AddDays(1));
        }

        public static PersianDateTime operator --(PersianDateTime d1)
        {
            var md1 = PersianToMiladi(d1);
            return MiladiToPersian(md1.AddDays(-1));
        }

        #endregion
    }
}