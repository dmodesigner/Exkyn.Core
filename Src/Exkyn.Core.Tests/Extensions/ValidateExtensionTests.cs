using Exkyn.Core.Extensions;

namespace Exkyn.Core.Tests.Extensions;

public class ValidateExtensionTests
{
    #region ValidateCPF Tests

    [Theory]
    [InlineData("52998224725")]
    [InlineData("529.982.247-25")]
    [InlineData("11144477735")]
    [InlineData("111.444.777-35")]
    public void ValidateCPF_ShouldReturnTrue_WhenCpfIsValid(string validCpf)
    {
        Assert.True(validCpf.ValidateCPF());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("12345678909")] // Sequência bloqueada explicitamente
    [InlineData("000.000.000-00")] // Todos iguais
    [InlineData("111.111.111-11")] // Todos iguais
    [InlineData("529.982.247-20")] // Dígito verificador incorreto
    [InlineData("12345")] // Tamanho menor que 11
    [InlineData("1234567890123")] // Tamanho maior que 11
    [InlineData("ABC.DEF.GHI-JK")] // Letras com 11 caracteres (não deve lançar FormatException)
    public void ValidateCPF_ShouldReturnFalse_WhenCpfIsInvalid(string? invalidCpf)
    {
        Assert.False(invalidCpf.ValidateCPF());
    }

    #endregion

    #region ValidateCNPJ Tests

    [Theory]
    [InlineData("11222333000181")]
    [InlineData("11.222.333/0001-81")]
    [InlineData("04.252.011/0001-10")]
    public void ValidateCNPJ_ShouldReturnTrue_WhenCnpjIsValid(string validCnpj)
    {
        Assert.True(validCnpj.ValidateCNPJ());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("00.000.000/0000-00")] // Todos iguais
    [InlineData("11.111.111/1111-11")] // Todos iguais
    [InlineData("11.222.333/0001-80")] // Dígito verificador inválido
    [InlineData("11.222.333/0001")] // Tamanho inválido
    [InlineData("AB.CDE.FGH/IJKL-MN")] // Letras com 14 caracteres (não deve lançar FormatException)
    public void ValidateCNPJ_ShouldReturnFalse_WhenCnpjIsInvalid(string? invalidCnpj)
    {
        Assert.False(invalidCnpj.ValidateCNPJ());
    }

    #endregion

    #region ValidatePIS Tests

    [Theory]
    [InlineData("12056874107")]
    [InlineData("120.56874.10-7")]
    [InlineData("201.49188.69-7")] // Corrigido: dígito verificador real é 7
    [InlineData("170.33259.50-4")] // Soma = 194, 194 % 11 = 7, 11 - 7 = 4
    public void ValidatePIS_ShouldReturnTrue_WhenPisIsValid(string validPis)
    {
        Assert.True(validPis.ValidatePIS());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("000.00000.00-0")] // Todos iguais
    [InlineData("120.56874.10-0")] // Dígito verificador incorreto
    [InlineData("201.49188.69-5")] // Dígito verificador incorreto
    [InlineData("123456")] // Tamanho incorreto
    [InlineData("ABC.DEFGH.IJ-K")] // Letras com 11 caracteres
    public void ValidatePIS_ShouldReturnFalse_WhenPisIsInvalid(string? invalidPis)
    {
        Assert.False(invalidPis.ValidatePIS());
    }

    #endregion

    #region ValidateEmail Tests

    [Theory]
    [InlineData("contato@exkyn.com")]
    [InlineData("daniel.moura@dominio.com.br")]
    [InlineData("user_name+tag@empresa.cloud")] // TLD > 4 letras e sinal de +
    [InlineData("suporte-ti@sub.dominio.org")]
    public void ValidateEmail_ShouldReturnTrue_WhenEmailIsValid(string validEmail)
    {
        Assert.True(validEmail.ValidateEmail());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("emailsemarroba.com")]
    [InlineData("@semusuario.com")]
    [InlineData("usuario@semdominio")]
    [InlineData("usuario@.com")]
    [InlineData(".usuario@dominio.com")]
    public void ValidateEmail_ShouldReturnFalse_WhenEmailIsInvalid(string? invalidEmail)
    {
        Assert.False(invalidEmail.ValidateEmail());
    }

    #endregion
}