using Exkyn.Core.Helpers;

namespace Exkyn.Core.Tests.Helpers;

public class DirectoryHelperTests : IDisposable
{
    private readonly string _tempRootDir;

    public DirectoryHelperTests()
    {
        _tempRootDir = Path.Combine(Path.GetTempPath(), $"ExkynTests_{Guid.NewGuid():N}");
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempRootDir))
        {
            Directory.Delete(_tempRootDir, recursive: true);
        }
        GC.SuppressFinalize(this);
    }

    [Theory]
    [InlineData(@"C:\Projetos\Exkyn", @"C:\Projetos\Exkyn\")]
    [InlineData(@"C:\Projetos\Exkyn\", @"C:\Projetos\Exkyn\")]
    [InlineData(@"  C:\Projetos\Exkyn  ", @"C:\Projetos\Exkyn\")]
    public void AddSlashEnd_ShouldEnsureSingleTrailingBackslash(string input, string expected)
    {
        // Act
        var result = DirectoryHelper.AddSlashEnd(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AddSlashEnd_ShouldThrowArgumentException_WhenDirectoryIsNullOrWhiteSpace(string? invalidDir)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => DirectoryHelper.AddSlashEnd(invalidDir!));
        Assert.Contains("O diretório não pode ser vazio ou nulo.", ex.Message);
    }

    [Fact]
    public void Create_ShouldCreateDirectory_WhenItDoesNotExist()
    {
        // Arrange
        var newDir = Path.Combine(_tempRootDir, "NovaPasta", "SubPasta");
        Assert.False(Directory.Exists(newDir));

        // Act
        DirectoryHelper.Create(newDir);

        // Assert
        Assert.True(Directory.Exists(newDir));
    }

    [Fact]
    public void Create_ShouldNotThrow_WhenDirectoryAlreadyExists()
    {
        // Arrange
        DirectoryHelper.Create(_tempRootDir);

        // Act & Assert
        var exception = Record.Exception(() => DirectoryHelper.Create(_tempRootDir));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_ShouldThrowArgumentException_WhenDirectoryIsNullOrWhiteSpace(string? invalidDir)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => DirectoryHelper.Create(invalidDir!));
    }

    [Fact]
    public void ListDirectoryFile_ShouldReturnAllFileNames_IncludingSubdirectories()
    {
        // Arrange
        var subDir = Path.Combine(_tempRootDir, "Sub");
        DirectoryHelper.Create(subDir);

        var file1 = Path.Combine(_tempRootDir, "arquivo1.txt");
        var file2 = Path.Combine(subDir, "arquivo2.log");

        File.WriteAllText(file1, "conteudo 1");
        File.WriteAllText(file2, "conteudo 2");

        // Act
        var result = DirectoryHelper.ListDirectoryFile(_tempRootDir);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains("arquivo1.txt", result);
        Assert.Contains("arquivo2.log", result);
    }

    [Fact]
    public void ListDirectoryFile_ShouldThrowDirectoryNotFoundException_WhenDirectoryDoesNotExist()
    {
        // Arrange
        var nonExistentDir = Path.Combine(_tempRootDir, "NaoExiste");

        // Act & Assert
        Assert.Throws<DirectoryNotFoundException>(() => DirectoryHelper.ListDirectoryFile(nonExistentDir));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ListDirectoryFile_ShouldThrowArgumentException_WhenDirectoryIsNullOrWhiteSpace(string? invalidDir)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => DirectoryHelper.ListDirectoryFile(invalidDir!));
    }
}