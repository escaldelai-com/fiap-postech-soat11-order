using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.Services;

public interface IOrderGetService
{

    Task<OrderInfoDto> Get(string orderId);

}
