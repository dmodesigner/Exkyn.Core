namespace Exkyn.Core.Extensions;

/// <summary>
/// Métodos de extensão para cálculo de datas de vencimento em dias corridos e dias úteis.
/// </summary>
public static class ExpirationDayExtension
{
    /// <summary>
    /// Acrescenta dias corridos à data fornecida.
    /// </summary>
    /// <param name="date">Data base inicial.</param>
    /// <param name="day">Quantidade de dias corridos a acrescentar.</param>
    /// <returns>Data resultante após o acréscimo de dias corridos.</returns>
    public static DateTime CalendarDays(this DateTime date, int day) => date.AddDays(day);

    /// <summary>
    /// Acrescenta dias úteis à data fornecida (desconsiderando sábados e domingos).
    /// </summary>
    /// <param name="date">Data base inicial.</param>
    /// <param name="day">Quantidade de dias úteis a acrescentar (deve ser maior ou igual a zero).</param>
    /// <returns>Data resultante em um dia útil.</returns>
    public static DateTime WorkingDays(this DateTime date, int day)
        => date.WorkingDays(day, null);

    /// <summary>
    /// Acrescenta dias úteis à data fornecida, desconsiderando sábados, domingos e feriados informados.
    /// </summary>
    /// <param name="date">Data base inicial.</param>
    /// <param name="day">Quantidade de dias úteis a acrescentar (deve ser maior ou igual a zero).</param>
    /// <param name="holidays">Lista opcional de feriados a serem ignorados na contagem.</param>
    /// <returns>Data resultante em um dia útil fora de feriados.</returns>
    public static DateTime WorkingDays(this DateTime date, int day, IEnumerable<DateTime>? holidays)
    {
        if (day < 0)
            throw new ArgumentOutOfRangeException(nameof(day), "A quantidade de dias úteis não pode ser negativa.");

        var holidaySet = holidays?
            .Select(h => h.Date)
            .ToHashSet() ?? [];

        var addedDays = 0;

        while (addedDays < day)
        {
            date = date.AddDays(1);

            var isWeekend = date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
            var isHoliday = holidaySet.Contains(date.Date);

            if (!isWeekend && !isHoliday)
            {
                addedDays++;
            }
        }

        return date;
    }
}