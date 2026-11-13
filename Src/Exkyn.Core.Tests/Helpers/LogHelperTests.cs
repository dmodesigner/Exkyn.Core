using Exkyn.Core.Helpers;

namespace Exkyn.Core.Tests.Helpers;

public class LogHelperTests : IDisposable
{
    private readonly string _tempDir;

    public LogHelperTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), $"ExkynLogTests_{Guid.NewGuid():N}");
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempDir))
        {
            Directory.Delete(_tempDir, recursive: true);
        }
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void Save_WithStringMessage_ShouldCreateFileAndWriteMessage()
    {
        // Arrange
        var fileName = "app.log";
        var logMessage = "Aplicação iniciada com sucesso.";

        // Act
        LogHelper.Save(_tempDir, fileName, logMessage);

        // Assert
        Assert.True(FileHelper.Exist(_tempDir, fileName));
        var content = FileHelper.ReadFile(_tempDir, fileName);
        Assert.Contains(logMessage, content);
    }

    [Fact]
    public void Save_WithException_ShouldWriteExceptionDetails_IncludingInnerExceptionsAndStackTrace()
    {
        // Arrange
        var fileName = "error.log";
        var inlineInner = new InvalidOperationException("Erro interno nível 2");
        var inner = new ArgumentException("Erro interno nível 1", inlineInner);
        Exception capturedException;

        try
        {
            var ex = new ApplicationException("Falha principal no processamento", inner)
            {
                Source = "Exkyn.Core.Tests",
                HelpLink = "https://docs.exkyn.com/errors/500"
            };
            throw ex;
        }
        catch (Exception ex)
        {
            capturedException = ex;
        }

        // Act
        LogHelper.Save(_tempDir, fileName, capturedException);

        // Assert
        var content = FileHelper.ReadFile(_tempDir, fileName);
        Assert.Contains("Falha principal no processamento", content);
        Assert.Contains("Source: Exkyn.Core.Tests", content);
        Assert.Contains("HelpLink: https://docs.exkyn.com/errors/500", content);
        Assert.Contains("InnerException: Erro interno nível 1", content);
        Assert.Contains("InnerException Inline: Erro interno nível 2", content);
        Assert.Contains("StackTrace:", content);
    }

    [Fact]
    public void Save_MultipleCalls_ShouldAppendLogsToSameFile()
    {
        // Arrange
        var fileName = "historico.log";

        // Act
        LogHelper.Save(_tempDir, fileName, "Primeiro evento");
        LogHelper.Save(_tempDir, fileName, "Segundo evento");

        // Assert
        var content = FileHelper.ReadFile(_tempDir, fileName);
        Assert.Contains("Primeiro evento", content);
        Assert.Contains("Segundo evento", content);
    }

    [Fact]
    public void Save_WithNullException_ShouldThrowArgumentNullException()
    {
        // Arrange
        Exception? nullException = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => LogHelper.Save(_tempDir, "error.log", nullException!));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Save_WithInvalidMessage_ShouldThrowArgumentException(string? invalidMessage)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => LogHelper.Save(_tempDir, "app.log", invalidMessage!));
    }

    [Theory]
    [InlineData(null, "app.log")]
    [InlineData("", "app.log")]
    [InlineData("   ", "app.log")]
    [InlineData(@"C:\Logs", null)]
    [InlineData(@"C:\Logs", "")]
    [InlineData(@"C:\Logs", "   ")]
    public void Save_WithInvalidDirectoryOrFile_ShouldThrowArgumentException(string? dir, string? file)
    {
        Assert.Throws<ArgumentException>(() => LogHelper.Save(dir!, file!, "Mensagem válida"));
        Assert.Throws<ArgumentException>(() => LogHelper.Save(dir!, file!, new Exception("Erro")));
    }
}