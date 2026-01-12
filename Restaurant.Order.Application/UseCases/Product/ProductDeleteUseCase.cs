using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.UseCases;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.UseCases;

public class ProductDeleteUseCase(
    IProductRepository repo) : IProductDeleteUseCase
{

    public async Task Delete(string id)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(id)
            .Validate();

        if (await repo.GetById(id) == null)
            throw new NotFoundException(id);

        await repo.Delete(id);
    }

}
