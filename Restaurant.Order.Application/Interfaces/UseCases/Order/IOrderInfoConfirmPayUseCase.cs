using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.UseCases;

public interface IOrderInfoConfirmPayUseCase
{

    Task<OrderInfoDto> ConfirmPay(string? orderId);

}
