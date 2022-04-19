namespace FolderOrganizerApplication.DateTimes
{
    public static class DateTimeHelper
    {
        //public static DateTime ToDateTime(this string text)
        //{

        //    var items = text.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

        //    return new System.Globalization.PersianCalendar().ToDateTime(items[0].ToInt32(), items[1].ToInt32(), items[2].ToInt32(), 0, 0, 0, 0);

        //}
        public static string GetPersianDate(this DateTime date)
        {
            var perconv = new System.Globalization.PersianCalendar();

            return string.Format("{0:0000}/{1:00}/{2:00}", perconv.GetYear(date), perconv.GetMonth(date),
                                 perconv.GetDayOfMonth(date));
        }

        public static string GetPersianDate(this DateTime? date)
        {
            var perconv = new System.Globalization.PersianCalendar();
            if (date.HasValue)
            {
                return string.Format("{0:0000}/{1:00}/{2:00}", perconv.GetYear(date.Value), perconv.GetMonth(date.Value),
                                      perconv.GetDayOfMonth(date.Value));
            }
            return null;
        }

        public static string GetPersianDateTime(this DateTime date)
        {
            var perconv = new System.Globalization.PersianCalendar();

            return string.Format("{0:0000}/{1:00}/{2:00}-{3}", perconv.GetYear(date), perconv.GetMonth(date),
                                 perconv.GetDayOfMonth(date),
                                 date.Hour + ":" + date.Minute + ":" + date.Second);
        }

        public static string AddPersianMonth(this DateTime date, int count)
        {
            var perconv = new System.Globalization.PersianCalendar();
            return perconv.AddMonths(date, count).GetPersianDate();
        }

        public static DateTime DateTimeAddPersianMonth(this DateTime date, int count)
        {
            var perconv = new System.Globalization.PersianCalendar();
            var dateTime = perconv.AddMonths(date, count);
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);

        }
       
    }
}
