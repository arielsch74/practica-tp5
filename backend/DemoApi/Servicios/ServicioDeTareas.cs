using DemoApi.Models;

namespace DemoApi.Servicios;

/// <summary>
/// Da de alta una tarea y avisa.
///
/// 🔴 OJO CON EL `new` DE ACÁ ADENTRO: es el punto de partida del TP5 (§3.0).
/// Tal como está escrito, este servicio se fabrica su propia dependencia, y por
/// eso no hay forma de testear `Crear` sin que salga un aviso de verdad. El
/// práctico pide justamente eso: abrirlo para que la dependencia entre desde
/// afuera, y recién ahí escribir el test con un mock.
/// </summary>
public class ServicioDeTareas
{
    private readonly INotificador _notificador = new NotificadorEmail();

    public Tarea Crear(string titulo)
    {
        var validacion = Logica.TareaValidator.Validar(titulo);
        if (!validacion.EsValida)
            throw new ArgumentException(validacion.Error);

        var tarea = new Tarea { Titulo = validacion.TituloNormalizado!, CreadaEl = DateTime.UtcNow };
        _notificador.Enviar($"Nueva tarea: {tarea.Titulo}");
        return tarea;
    }
}
