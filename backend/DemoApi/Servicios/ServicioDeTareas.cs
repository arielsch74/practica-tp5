using DemoApi.Models;

namespace DemoApi.Servicios;

/// <summary>
/// Da de alta una tarea y avisa.
///
/// 🔴 EL REFACTOR DEL TP5 (§3.0), YA HECHO: la dependencia ENTRA POR EL
/// CONSTRUCTOR. Antes decía `new NotificadorEmail()` acá adentro, y por eso no
/// había forma de testear `Crear` sin que saliera un aviso de verdad.
///
/// 🔴 LO QUE EL REFACTOR NO TOCA: la validación. Abrir la clase para poder
/// testearla no es una excusa para sacarle lo que hace — si el «después» pierde
/// una regla de negocio, el test con mock queda verde sobre un servicio que
/// dejó de validar.
/// </summary>
public class ServicioDeTareas
{
    private readonly INotificador _notificador;

    public ServicioDeTareas(INotificador notificador)     // ← ahora la RECIBE
    {
        _notificador = notificador;
    }

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
