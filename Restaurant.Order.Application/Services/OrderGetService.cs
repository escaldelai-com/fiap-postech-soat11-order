using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.Services;

public class OrderGetService(
    IOrderRepository repo) : IOrderGetService
{

    public async Task<OrderInfoDto> Get(string orderId)
    {
        var order = await repo.Get(orderId);

        if (order == null)
            throw new NotFoundException(orderId);

        return order;
    }

}
