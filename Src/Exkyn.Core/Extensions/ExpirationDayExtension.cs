namespace Exkyn.Core.Extensions;

public static class ExpirationDayExtension
{
    /// <summary>
    /// Acrescenta uma data corrida a data fornecida
    /// </summary>
    /// <param name="day">Recebe um int</param>
    /// <param name="date">Recebe um DateTime</param>
    /// <returns>Retorna um DateTime</returns>
    public static DateTime CalendarDays(this DateTime date, int day) => date.AddDays(day);

    /// <summary>
    /// Acrescenta dias a data levando em consideração dias uteis
    /// </summary>
    /// <param name="day">Recebe um int</param>
    /// <param name="date">Recebe um DateTime</param>
    /// <returns>Retorna um DateTime</returns>
    public static DateTime WorkingDays(this DateTime date, int day)
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
    public static DateTime WorkingDays(this DateTime date, int day, List<DateTime> holidays)
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
