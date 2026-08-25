namespace DemoApi.Servicios;

/// <summary>
/// Avisa que pasó algo. Es una INTERFAZ —un contrato— y no una clase: dice QUÉ
/// se puede pedir, no CÓMO se hace. Eso es lo que después permite que un test
/// le pase un impostor en lugar del que manda mails de verdad (TP5 §3.0).
/// </summary>
public interface INotificador
{
    void Enviar(string mensaje);
}
