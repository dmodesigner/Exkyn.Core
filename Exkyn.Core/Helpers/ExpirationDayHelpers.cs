using System;
using System.Collections.Generic;

namespace Exkyn.Core.Helpers
{
    public static class ExpirationDayHelpers
    {
        /// <summary>
        /// Acrescenta uma data corrida a data fornecida
        /// </summary>
        /// <param name="day">Recebe um int</param>
        /// <param name="date">Recebe um DateTime</param>
        /// <returns>Retorna um DateTime</returns>
        public static DateTime Run(int day, DateTime date) => date.AddDays(day);

        /// <summary>
        /// Acrescenta dias a data levando em consideração dias uteis
        /// </summary>
        /// <param name="day">Recebe um int</param>
        /// <param name="date">Recebe um DateTime</param>
        /// <returns>Retorna um DateTime</returns>
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

        /// <summary>
        /// Acrescenta dias a data levando em consideração dias uteis e feriados
        /// </summary>
        /// <param name="day">Recebe um int<</param>
        /// <param name="date">Recebe um DateTime</param>
        /// <param name="holidays">Recebe uma lista de DateTime</param>
        /// <returns>Retorna um DateTime</returns>
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