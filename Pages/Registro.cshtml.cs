using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VerificacionSignalR.Services;

namespace VerificacionSignalR.Pages;

public class RegistroModel : PageModel
{
    private readonly UsuarioStore _store;
    public RegistroModel(UsuarioStore store) => _store = store;

    [BindProperty] public string Email { get; set; } = "";

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Email)) return Page();
        _store.Registrar(Email);
        return RedirectToPage("Esperando", new { email = Email });
    }
}