using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.UseCases;

public interface IOrderInfoCreateUseCase
{

    Task<OrderInfoDto> CreateByCpf(string cpf);

    Task<OrderInfoDto> CreateById(string? clientId);

}
