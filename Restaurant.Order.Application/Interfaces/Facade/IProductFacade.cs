using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.Facade;

public interface IProductFacade
{

    Task<ProductDto?> GetById(string? id);

    Task<IEnumerable<ProductDto>> GetByType(string type);

    Task<string?> Create(ProductDto product);

    Task<string?> Update(ProductDto product);


    Task Delete(string id);

}
