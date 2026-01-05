using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.Services;

public interface IOrderItemGetService
{

    Task<OrderItemDto> Get(string itemId);

}
