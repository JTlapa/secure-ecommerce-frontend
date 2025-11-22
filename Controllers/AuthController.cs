using System.Security.Claims;
using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet.Controllers;

public class AuthController(AuthClientService auth, UsuariosClientService usuarios) : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IndexAsync(Login model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Esta función verifica en backend que el correo y contraseña sean válidos
                var token = await auth.ObtenTokenAsync(model.Email, model.Password);
                var claims = new List<Claim>
                {
                    // Todo esto se guarda en la Cookie
                    new(ClaimTypes.Name, token.Email),
                    new(ClaimTypes.GivenName, token.Nombre),
                    new("jwt", token.Jwt),
                    new(ClaimTypes.Role, token.Rol),
                };

                auth.IniciaSesionAsync(claims);

                // Usuario válido
                if (token.Rol == "Administrador")
                    return RedirectToAction("Index", "Productos");
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Email", "Credenciales no válidas. Inténtelo nuevamente.");
            }
        }
        return View(model);
    }

    [Authorize(Roles = "Administrador, Usuario")]
    public async Task<IActionResult> SalirAsync()
    {
        // Cierra la sesión
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        // Sino, se redirige a la página inicial
        return RedirectToAction("Index", "Auth");
    }

    [AllowAnonymous]
    public ActionResult CrearCuenta()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearCuentaAsync(UsuarioPwd itemToCreate)
    {
        itemToCreate.Rol = "Usuario";
        ModelState.Remove("Rol");
        if (ModelState.IsValid)
        {
            try
            {
                await usuarios.PostAsync(itemToCreate);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
            }
        }
        ModelState.AddModelError("Password", "No ha sido posible crear la cuenta. Inténtelo nuevamente.");
        return View(itemToCreate);
    }
}