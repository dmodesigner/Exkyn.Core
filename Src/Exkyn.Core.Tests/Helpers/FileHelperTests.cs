using System.Text;
using Exkyn.Core.Helpers;

namespace Exkyn.Core.Tests.Helpers;

public class FileHelperTests : IDisposable
{
    private readonly string _tempDir;

    public FileHelperTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), $"ExkynFileTests_{Guid.NewGuid():N}");
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
    public void Create_ShouldCreateEmptyFileAndDirectory_WhenTheyDoNotExist()
    {
        // Arrange
        var fileName = "teste.txt";

        // Act
        FileHelper.Create(_tempDir, fileName);

        // Assert
        Assert.True(FileHelper.Exist(_tempDir, fileName));
        Assert.Equal(string.Empty, FileHelper.ReadFile(_tempDir, fileName));
    }

    [Fact]
    public void Exist_ShouldReturnFalse_WhenFileDoesNotExist()
    {
        // Act
        var exists = FileHelper.Exist(_tempDir, "inexistente.txt");

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public void Write_And_ReadFile_ShouldAppendLinesAndReadCorrectly_EvenWithoutTrailingSlashInDirectory()
    {
        // Arrange
        var dirWithoutSlash = _tempDir.TrimEnd('\\', '/');
        var fileName = "log.txt";

        // Act
        FileHelper.Write(dirWithoutSlash, fileName, "Linha 1");
        FileHelper.Write(dirWithoutSlash, fileName, "Linha 2");

        var content = FileHelper.ReadFile(dirWithoutSlash, fileName);

        // Assert
        Assert.Contains("Linha 1", content);
        Assert.Contains("Linha 2", content);
    }

    [Fact]
    public void Write_WithCustomEncoding_ShouldWriteUsingSpecifiedEncoding()
    {
        // Arrange
        var fileName = "latin1.txt";
        var text = "Acentuação: São Paulo, Coração";

        // Act
        FileHelper.Write(_tempDir, fileName, text, Encoding.Latin1);

        // Assert
        var fullPath = Path.Combine(_tempDir, fileName);
        var readBack = File.ReadAllText(fullPath, Encoding.Latin1);
        Assert.Contains(text, readBack);
    }

    [Fact]
    public void ReadFile_ShouldThrowArgumentException_WhenFileDoesNotExist()
    {
        // Arrange
        DirectoryHelper.Create(_tempDir);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => FileHelper.ReadFile(_tempDir, "nao_existe.txt"));
        Assert.Contains("não foi encontrado", ex.Message);
    }

    [Theory]
    [InlineData(null, "arquivo.txt")]
    [InlineData("", "arquivo.txt")]
    [InlineData("   ", "arquivo.txt")]
    [InlineData(@"C:\Temp", null)]
    [InlineData(@"C:\Temp", "")]
    [InlineData(@"C:\Temp", "   ")]
    public void Methods_ShouldThrowArgumentException_WhenDirectoryOrFileIsNullOrWhiteSpace(string? dir, string? file)
    {
        Assert.Throws<ArgumentException>(() => FileHelper.Exist(dir!, file!));
        Assert.Throws<ArgumentException>(() => FileHelper.Create(dir!, file!));
        Assert.Throws<ArgumentException>(() => FileHelper.Write(dir!, file!, "conteudo"));
        Assert.Throws<ArgumentException>(() => FileHelper.ReadFile(dir!, file!));
    }
}