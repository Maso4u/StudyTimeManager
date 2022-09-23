using System;

namespace Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBetweenTwoDates(this DateOnly date, DateOnly startDate, DateOnly endDate)
        {
            return date >= startDate && date <= endDate;
        }
        public static bool IsBetweenTwoDates(this DateTime date, DateTime startDate, DateTime endDate)
        {
            return date >= startDate && date <= endDate;
        }
    }
}
