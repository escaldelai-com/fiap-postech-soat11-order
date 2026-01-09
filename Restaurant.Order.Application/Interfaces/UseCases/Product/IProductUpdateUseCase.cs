using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.UseCases;

public interface IProductUpdateUseCase
{

    Task<string?> Update(ProductDto product);

}
