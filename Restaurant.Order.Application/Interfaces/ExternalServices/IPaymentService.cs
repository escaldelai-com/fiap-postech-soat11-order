using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.ExternalServices;

public interface IPaymentService
{

    Task Pay(OrderInfoDto order);

}
