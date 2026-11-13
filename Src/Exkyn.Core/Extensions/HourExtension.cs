namespace Exkyn.Core.Extensions;

/// <summary>
/// Métodos de extensão para manipulação do horário inicial e final de um <see cref="DateTime"/>.
/// </summary>
public static class HourExtension
{
    /// <summary>
    /// Retorna a data informada com hora, minuto, segundo e milissegundo zerados (00:00:00).
    /// </summary>
    /// <param name="date">Data base a ser ajustada.</param>
    /// <returns>Um <see cref="DateTime"/> Representando o início do dia (00:00:00).</returns>
    public static DateTime StartTime(this DateTime date) => date.Date;

    /// <summary>
    /// Retorna a data informada com a última hora, minuto e segundo do dia (23:59:59).
    /// </summary>
    /// <param name="date">Data base a ser ajustada.</param>
    /// <returns>Um <see cref="DateTime"/> Representando o final do dia (23:59:59).</returns>
    public static DateTime EndTime(this DateTime date) => date.Date.AddDays(1).AddSeconds(-1);
}