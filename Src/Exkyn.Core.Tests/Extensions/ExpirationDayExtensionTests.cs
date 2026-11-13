using Exkyn.Core.Extensions;

namespace Exkyn.Core.Tests.Extensions;

public class ExpirationDayExtensionTests
{
    [Fact]
    public void CalendarDays_ShouldAddExactDays_IncludingWeekends()
    {
        // Arrange: Sexta-feira, 13 de Novembro de 2026
        var startDate = new DateTime(2026, 11, 13);

        // Act
        var result = startDate.CalendarDays(2);

        // Assert: Domingo, 15 de Novembro de 2026
        Assert.Equal(new DateTime(2026, 11, 15), result);
    }

    [Fact]
    public void WorkingDays_ShouldSkipWeekend_WhenStartingOnFriday()
    {
        // Arrange: Sexta-feira, 13 de Novembro de 2026
        var friday = new DateTime(2026, 11, 13);

        // Act: +1 dia útil deve pular Sábado (14) e Domingo (15), caindo na Segunda (16)
        var result = friday.WorkingDays(1);

        // Assert
        Assert.Equal(new DateTime(2026, 11, 16), result);
        Assert.Equal(DayOfWeek.Monday, result.DayOfWeek);
    }

    [Fact]
    public void WorkingDays_ShouldAddMultipleWeeksCorrectly()
    {
        // Arrange: Segunda-feira, 2 de Novembro de 2026
        var monday = new DateTime(2026, 11, 2);

        // Act: +5 dias úteis (Terça 3, Quarta 4, Quinta 5, Sexta 6, Segunda 9)
        var result = monday.WorkingDays(5);

        // Assert
        Assert.Equal(new DateTime(2026, 11, 9), result);
    }

    [Fact]
    public void WorkingDays_WithHolidays_ShouldSkipWeekendsAndHolidays_EvenWithDifferentTimeComponents()
    {
        // Arrange: Sexta-feira, 13 de Novembro de 2026 às 14:30
        var startDate = new DateTime(2026, 11, 13, 14, 30, 0);

        // Feriado fictício na Segunda-feira (16/11/2026) com hora zerada
        var holidays = new List<DateTime>
        {
            new(2026, 11, 16)
        };

        // Act: +1 dia útil -> pula Sábado (14), Domingo (15) e Feriado na Segunda (16) -> Terça (17)
        var result = startDate.WorkingDays(1, holidays);

        // Assert: Terça-feira, 17/11/2026 mantendo o horário original (14:30)
        Assert.Equal(new DateTime(2026, 11, 17, 14, 30, 0), result);
    }

    [Fact]
    public void WorkingDays_WhenDayIsZero_ShouldReturnSameDate()
    {
        // Arrange
        var startDate = new DateTime(2026, 11, 13);

        // Act
        var result = startDate.WorkingDays(0);

        // Assert
        Assert.Equal(startDate, result);
    }

    [Fact]
    public void WorkingDays_WhenDayIsNegative_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var startDate = new DateTime(2026, 11, 13);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => startDate.WorkingDays(-1));
    }
}