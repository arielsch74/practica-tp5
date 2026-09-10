using DemoApi.Logica;

namespace DemoApi.Tests;

public class TareaValidatorTests
{
    [Fact]
    public void TituloValido_EsAceptado_YSeNormalizaConTrim()
    {
        var resultado = TareaValidator.Validar("  Preparar la demo de la clase  ");

        Assert.True(resultado.EsValida);
        Assert.Null(resultado.Error);
        Assert.Equal("Preparar la demo de la clase", resultado.TituloNormalizado);
    }



    [Fact]
    public void TituloQueSuperaElLargoMaximo_EsRechazado()
    {
        var titulo = new string('a', TareaValidator.LargoMaximo + 1);

        var resultado = TareaValidator.Validar(titulo);

        Assert.False(resultado.EsValida);
        Assert.Contains($"{TareaValidator.LargoMaximo}", resultado.Error);
    }

    [Theory]
    [InlineData("")]            // vacío
    [InlineData("   ")]         // sólo espacios
    [InlineData("\t")]          // un tabulador
    public void TituloSinContenido_EsRechazado(string? titulo)
    {
        var resultado = TareaValidator.Validar(titulo);

        Assert.False(resultado.EsValida);
    }

    [Fact]
    public void TituloDemasiadoLargo_ExplicaElLimiteEnElMensaje()
    {
        var titulo = new string('a', TareaValidator.LargoMaximo + 1);   // 101: uno más que el tope

        var resultado = TareaValidator.Validar(titulo);

        Assert.False(resultado.EsValida);
        Assert.Contains(TareaValidator.LargoMaximo.ToString(), resultado.Error);
    }
}
