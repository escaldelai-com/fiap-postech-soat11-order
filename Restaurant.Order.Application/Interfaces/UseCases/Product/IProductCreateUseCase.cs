using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.UseCases;

public interface IProductCreateUseCase
{

    Task<string?> Create(ProductDto product);

}
