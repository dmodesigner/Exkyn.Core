using Exkyn.Core.Extensions;

namespace Exkyn.Core.Tests.Extensions;

public class HourExtensionTests
{
    [Fact]
    public void StartTime_ShouldReturnDateWithZeroedTime()
    {
        // Arrange
        var input = new DateTime(2026, 11, 13, 16, 45, 32, 500, DateTimeKind.Utc);

        // Act
        var result = input.StartTime();

        // Assert
        Assert.Equal(new DateTime(2026, 11, 13, 0, 0, 0, DateTimeKind.Utc), result);
        Assert.Equal(0, result.Hour);
        Assert.Equal(0, result.Minute);
        Assert.Equal(0, result.Second);
        Assert.Equal(0, result.Millisecond);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void EndTime_ShouldReturnDateWith23Hours59Minutes59Seconds()
    {
        // Arrange
        var input = new DateTime(2026, 11, 13, 08, 15, 10, 123, DateTimeKind.Local);

        // Act
        var result = input.EndTime();

        // Assert
        Assert.Equal(new DateTime(2026, 11, 13, 23, 59, 59, DateTimeKind.Local), result);
        Assert.Equal(23, result.Hour);
        Assert.Equal(59, result.Minute);
        Assert.Equal(59, result.Second);
        Assert.Equal(0, result.Millisecond);
        Assert.Equal(DateTimeKind.Local, result.Kind);
    }

    [Fact]
    public void StartTime_And_EndTime_ShouldPreserveSameCalendarDate()
    {
        // Arrange
        var input = new DateTime(2026, 2, 28, 12, 0, 0);

        // Act
        var start = input.StartTime();
        var end = input.EndTime();

        // Assert
        Assert.Equal(input.Year, start.Year);
        Assert.Equal(input.Month, start.Month);
        Assert.Equal(input.Day, start.Day);

        Assert.Equal(input.Year, end.Year);
        Assert.Equal(input.Month, end.Month);
        Assert.Equal(input.Day, end.Day);
        Assert.True(end > start);
    }
}