using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class Pedido
{
    [Display(Name = "Id")]
    public string? Id { get; set; }
    [Display(Name = "Cliente")]
    public string? Cliente { get; set; }
    [Display(Name = "Fecha")]
    public DateTime? Fecha { get; set; }
    [Display(Name = "Productos")]
    public List<ProductoPedido> Productos { get; set; } = [];
    [Display(Name = "Total")]
    public decimal Total 
    {
        get
        {
            decimal total = 0;
            foreach (ProductoPedido producto in Productos)
            {
                var totalProductos = producto.Precio * producto.Cantidad;
                total += totalProductos ?? 0;
            } 
            return total;
        }
    }
    
}

public record ProductoPedido
{
    [Display(Name = "Id de Producto")]
    public string? Id { get; set; }
    [Display(Name = "Titulo")]
    public string? Titulo { get; set; }
    [Display(Name = "Cantidad")]
    public int? Cantidad { get; set; }
    [Display(Name = "Precio")]
    public decimal? Precio { get; set; }
    [Display(Name = "Subtotal")]
    public decimal? Subtotal { get; set; }
}