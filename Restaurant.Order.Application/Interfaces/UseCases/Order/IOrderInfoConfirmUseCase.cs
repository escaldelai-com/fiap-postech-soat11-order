using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.UseCases;

public interface IOrderInfoConfirmUseCase
{

    Task<OrderInfoDto> Confirm(string? orderId);

}
