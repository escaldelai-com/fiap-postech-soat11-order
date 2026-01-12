using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.Services;

public class OrderItemGetService(
    IProductRepository repo) : IOrderItemGetService
{

    public async Task<OrderItemDto> GetById(string itemId)
    {
        var item = await repo.GetById(itemId);

        return item == null
            ? throw new NotFoundException(itemId)
            : new OrderItemDto
            {
                Id = item.Id,
                Nome = item.Nome,
                Tipo = item.Tipo,
                Preco = item.Preco
            };
    }


}
