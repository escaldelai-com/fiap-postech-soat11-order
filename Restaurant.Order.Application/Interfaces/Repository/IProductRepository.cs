using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.Repository;

public interface IProductRepository
{

    Task<ProductDto?> Get(string? id);

    Task<IEnumerable<ProductDto>> GetByType(string type);

    Task<string?> Create(ProductDto product);

    Task<string?> Update(ProductDto product);

    Task Delete(string id);

}
