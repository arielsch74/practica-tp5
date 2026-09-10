using DemoApi.Logica;
using Xunit;

namespace DemoApi.Tests;

public class TareaValidatorBordeTests
{
    [Fact]
    public void TituloDeLargoExacto_SeAcepta()      // el test que mata al mutante
    {
        var titulo = new string('a', TareaValidator.LargoMaximo);   // 100 justos

        var resultado = TareaValidator.Validar(titulo);

        Assert.True(resultado.EsValida);
    }
}
