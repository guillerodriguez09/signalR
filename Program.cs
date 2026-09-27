using Microsoft.AspNetCore.SignalR;
using VerificacionSignalR.Hubs;
using VerificacionSignalR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR();                   // SignalR
builder.Services.AddSingleton<UsuarioStore>();   // "base de datos" en memoria

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapHub<VerificacionHub>("/verificacionHub");  // endpoint del Hub

// Simula el click en el link del email
app.MapGet("/verificar", async (string email, UsuarioStore store,
                                IHubContext<VerificacionHub> hub) =>
{
    if (!store.Verificar(email))
        return Results.NotFound("Usuario no encontrado");

    await hub.Clients.Group(email).SendAsync("EmailVerificado");

    return Results.Content(
        "<h2>¡Email verificado! Ya podés volver a la otra pestaña.</h2>",
        "text/html; charset=utf-8");
});

app.Run();