using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.UseCases;

public interface IOrderInfoCreateUseCase
{

    Task<OrderInfoDto> Create(string cpf);

}
