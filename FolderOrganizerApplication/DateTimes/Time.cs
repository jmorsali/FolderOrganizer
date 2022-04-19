namespace FolderOrganizerApplication.DateTimes
{
    public static class Time
    {

        public static DateTime SetTime(this DateTime? date, string timeString)
        {
            if (!date.HasValue)
                throw new InvalidOperationException();
            try
            {
                DateTime dt = DateTime.Parse(timeString);

                int Hour = int.Parse(dt.ToString("HH"));
                var Minute = int.Parse(dt.ToString("mm"));
                
                var time = new TimeSpan(Hour, Minute, 0);
                return date.Value.Date.Add(time);
            }
            catch
            {
                return date.Value;
            }
            
        }

        public static DateTime? TrySetTime(this DateTime? date, string timeString)
        {
            try
            {
                return date.SetTime(timeString);
            }
            catch
            {
                return date;
            }
        }

        public static bool HasTime(this DateTime? date)
        {
            if (!date.HasValue)
                return false;

            if(date.Value.Hour!=0 || date.Value.Minute!=0 || date.Value.Second!=0)
                return true;
            return false;
        }
        
    }
}
