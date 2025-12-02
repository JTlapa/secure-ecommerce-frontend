namespace frontendnet.Models;

public class ProductoCarrito
{
    public string IdCarrito { get; set; } = "";
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }

    // Puedes agregar esto si tu API lo regresa:
    public Producto? Producto { get; set; }
}
