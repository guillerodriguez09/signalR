using Microsoft.AspNetCore.SignalR;
using VerificacionSignalR.Services;

namespace VerificacionSignalR.Hubs;

public class VerificacionHub : Hub
{
    private readonly UsuarioStore _store;
    public VerificacionHub(UsuarioStore store) => _store = store;

    public async Task EsperarVerificacion(string email)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, email);

        if (_store.EstaVerificado(email))
            await Clients.Caller.SendAsync("EmailVerificado");
    }
}