using frontendnet.Models;

namespace frontendnet.Services;

public class PedidosClientService(HttpClient client)
{
    public async Task<List<Pedido>?> GetAllAsync()
    {
        return await client.GetFromJsonAsync<List<Pedido>>("api/pedido");
    } 
}