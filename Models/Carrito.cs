namespace frontendnet.Models;

public class Carrito
{
    public string Id { get; set; } = "";
    public string Email { get; set; } = "";
    public bool Actual { get; set; }
    public DateTime? FechaCompra { get; set; }

    public List<ProductoCarrito> ItemsCarrito { get; set; } = new();
    public decimal Total { get; set; }
}
