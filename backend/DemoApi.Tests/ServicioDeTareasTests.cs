using DemoApi.Servicios;
using Moq;
using Xunit;

namespace DemoApi.Tests;

public class ServicioDeTareasTests
{
    [Fact]
    public void CrearUnaTarea_NotificaUnaSolaVez()
    {
        // Arrange: el impostor, en lugar del notificador real
        var notificador = new Mock<INotificador>();
        var servicio = new ServicioDeTareas(notificador.Object);

        // Act
        servicio.Crear("Comprar café");

        // Assert: no mira un valor devuelto — mira la INTERACCIÓN
        notificador.Verify(n => n.Enviar(It.IsAny<string>()), Times.Once);
    }
}
