using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.UseCases;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.UseCases;

public class ProductDeleteUseCase(
    IProductRepository repo) : IProductDeleteUseCase
{

    public async Task Delete(string id)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(id)
            .Validate();

        var exists = await repo.Get(id);

        if (exists == null)
            throw new NotFoundException(id);

        await repo.Delete(id);
    }

}
