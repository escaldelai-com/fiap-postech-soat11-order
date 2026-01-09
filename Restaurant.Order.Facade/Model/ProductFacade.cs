using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Facade;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.UseCases;

namespace Restaurant.Order.Facade;

public class ProductFacade(
    IProductRepository repo,
    IProductCreateUseCase createUseCase,
    IProductUpdateUseCase updateUseCase,
    IProductDeleteUseCase deleteUseCase) : IProductFacade
{

    public Task<ProductDto?> Get(string? id)
    {
        return repo.Get(id);
    }

    public Task<IEnumerable<ProductDto>> GetByType(string type)
    {
        return repo.GetByType(type);
    }

    public async Task<string?> Create(ProductDto product)
    {
        return await createUseCase.Create(product);
    }

    public async Task<string?> Update(ProductDto product)
    {
        return await updateUseCase.Update(product);
    }

    public async Task Delete(string id)
    {
        await deleteUseCase.Delete(id);
    }

}
