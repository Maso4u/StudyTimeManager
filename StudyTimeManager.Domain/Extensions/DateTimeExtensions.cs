using System;

namespace StudyTimeManager.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBetweenTwoDates(this DateTime date, DateTime startDate, DateTime endDate)
        {
            return date >= startDate && date <= endDate;
        }

        public static DateTime ToDateTime(this DateOnly? date)
        {
           return (DateTime)(date?.ToDateTime(TimeOnly.MinValue));
        }
    }
}
