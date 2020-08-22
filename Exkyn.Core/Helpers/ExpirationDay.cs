using System;
using System.Collections.Generic;

namespace Exkyn.Core.Helpers
{
    public static class ExpirationDay
    {
        public static DateTime Run(int day, DateTime date) => date.AddDays(day);

        public static DateTime Useful(int day, DateTime date)
        {
            var count = 1;

            while (count <= day)
            {
                var weekday = true;

                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    weekday = false;

                if (weekday)
                    count++;

                date = date.AddDays(1);
            }

            return date;
        }

        public static DateTime Useful(int day, DateTime date, List<DateTime> holidays)
        {
            var count = 1;

            while (count <= day)
            {
                var weekday = true;

                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    weekday = false;

                else if (holidays.Contains(date))
                    weekday = false;

                if (weekday)
                    count++;

                date = date.AddDays(1);
            }

            return date;
        }
    }
}