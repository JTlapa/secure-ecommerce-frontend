using System.Net.Http.Json;
using frontendnet.Models;


namespace frontendnet.Services;

public class CarritoService
{
    private readonly HttpClient http;
    private readonly string baseUrl;

    public CarritoService(HttpClient http)
    {
        this.http = http;
        baseUrl = "/api/carrito";  // porque ya tienes BaseAddress desde Program.cs
    }


    // GET /carrito/actual
    public async Task<Carrito?> GetActualAsync(string email)
    {
        var response = await http.GetAsync($"{baseUrl}/actual?email={email}");
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Error al obtener carrito", null, response.StatusCode);

        return await response.Content.ReadFromJsonAsync<Carrito>();
    }

    // POST /carrito/{idcarrito}/productos
    public async Task AgregarProductoAsync(string idCarrito, ProductoCarrito producto)
    {
        Console.WriteLine("AgregarProductoAsync llamado");
        var body = new {
            idproducto = producto.IdProducto,
            cantidad = producto.Cantidad
        };
        var response = await http.PostAsJsonAsync(
            $"{baseUrl}/{idCarrito}/productos", body
        );
        Console.WriteLine(response.StatusCode);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Error al agregar producto", null, response.StatusCode);
    }


    // DELETE /carrito/{idcarrito}/productos/{idproducto}
    public async Task QuitarProductoAsync(string idCarrito, int idProducto)
    {
        var response = await http.DeleteAsync(
            $"{baseUrl}/{idCarrito}/productos/{idProducto}"
        );

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Error al quitar producto", null, response.StatusCode);
    }

    // PUT /carrito/comprar/{idcarrito}
    public async Task ComprarAsync(string idCarrito, Carrito carrito)
    {
        var response = await http.PutAsJsonAsync(
            $"{baseUrl}/comprar/{idCarrito}", carrito
        );

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Error al comprar", null, response.StatusCode);
    }

    // PUT /carrito/{idcarrito}/productos/{idproducto}
    public async Task ModificarCantidadAsync(string idCarrito, int idProducto, int cantidad)
    {
        var body = new { cantidad = cantidad };

        var response = await http.PutAsJsonAsync(
            $"{baseUrl}/{idCarrito}/productos/{idProducto}", 
            body
        );

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Error al modificar cantidad", null, response.StatusCode);
    }

}
