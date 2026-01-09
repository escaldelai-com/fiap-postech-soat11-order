using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.Services;

public class OrderItemGetService(
    IProductRepository repo) : IOrderItemGetService
{

    public async Task<OrderItemDto> Get(string itemId)
    {
        var item = await repo.Get(itemId);

        if (item == null)
            throw new NotFoundException(itemId);

        return new OrderItemDto
        {
            Id = item.Id,
            Nome = item.Nome,
            Tipo = item.Tipo,
            Preco = item.Preco
        };
    }


}
