using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.UseCases;

public interface IOrderInfoAddItemUseCase
{

    Task<OrderInfoDto> AddItem(string? orderId, string? itemId);

}
