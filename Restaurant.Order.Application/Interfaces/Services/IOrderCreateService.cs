using Restaurant.Order.Application.DTO;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.Interfaces.Services;

public interface IOrderCreateService
{

    OrderInfo Create(OrderInfoDto orderDto);

}
