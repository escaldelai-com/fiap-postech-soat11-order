using Restaurant.Order.Application.DTO;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.Interfaces.Services;

public interface IOrderCreateService
{

    OrderInfo Create(OrderInfoDto orderDto);

}
