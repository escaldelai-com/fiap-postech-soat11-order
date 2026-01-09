using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.UseCases;

public interface IOrderInfoCancelUseCase
{

    Task<OrderInfoDto> Cancel(string? orderId);

}
