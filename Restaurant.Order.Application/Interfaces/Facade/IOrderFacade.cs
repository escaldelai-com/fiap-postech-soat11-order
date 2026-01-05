using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.Facade;

public interface IOrderFacade
{

    Task<IEnumerable<OrderInfoDto>> GetWaiting();

    Task<OrderInfoDto> Create(string cpf);

    Task<OrderInfoDto> AddItem(string? orderId, string? itemId);

    Task<OrderInfoDto> Confirm(string? orderId);

    Task<OrderInfoDto> Cancel(string? orderId);

    Task<OrderInfoDto> ConfirmPay(string? orderId);

}
