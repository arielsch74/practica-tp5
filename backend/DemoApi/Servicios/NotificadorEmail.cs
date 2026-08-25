namespace DemoApi.Servicios;

/// <summary>La implementación de verdad: la que usa la aplicación cuando corre.</summary>
public class NotificadorEmail : INotificador
{
    public void Enviar(string mensaje) => Console.WriteLine($"[email] {mensaje}");
}
