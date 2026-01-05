using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.Repository;

public interface IProductTypeRepository
{

    Task<IEnumerable<ProductTypeDto>> GetList();

}
