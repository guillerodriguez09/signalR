using System.Collections.Concurrent;

namespace VerificacionSignalR.Services;

public class UsuarioStore
{
    // email -> ¿está verificado?
    private readonly ConcurrentDictionary<string, bool> _usuarios = new();

    public void Registrar(string email) => _usuarios[email] = false;

    public bool EstaVerificado(string email) =>
        _usuarios.TryGetValue(email, out var verificado) && verificado;

    public bool Verificar(string email)
    {
        if (!_usuarios.ContainsKey(email)) return false;
        _usuarios[email] = true;
        return true;
    }
}