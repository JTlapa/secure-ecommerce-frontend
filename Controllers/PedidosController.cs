using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Administrador")]
public class PedidosController(PedidosClientService service) : Controller
{
    public async Task<IActionResult> IndexAsync()
    {
        List<Pedido>? listaPedidos = [];
        try
        {
            listaPedidos = await service.GetAllAsync();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        return View(listaPedidos);
    }
}