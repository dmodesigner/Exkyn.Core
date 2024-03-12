namespace Exkyn.Core.Extensions;

public static class HourExtension
{
    /// <summary>
    /// Retorna a data informada com hora, minuto e segundo zerado
    /// </summary>
    /// <param name="date">Recebe um DateTime</param>
    /// <returns>Retorna um DateTime</returns>
    public static DateTime StartTime(this DateTime date) => Convert.ToDateTime(string.Format("{0:yyyy-MM-dd 00:00:00}", date));

    /// <summary>
    /// Retorna a data informada com a última hora, minuto e segundo do dia
    /// </summary>
    /// <param name="date">ecebe um DateTime</param>
    /// <returns>ecebe um DateTime</returns>
    public static DateTime EndTime(this DateTime date) => Convert.ToDateTime(string.Format("{0:yyyy-MM-dd 23:59:59}", date));
}
