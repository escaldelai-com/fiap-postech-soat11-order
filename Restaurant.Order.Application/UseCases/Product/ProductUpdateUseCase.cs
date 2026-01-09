using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.UseCases;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application;

public class ProductUpdateUseCase(
    IProductRepository repo) : IProductUpdateUseCase
{

    public async Task<string?> Update(ProductDto product)
    {
        Validator.Create()
            .IsNotNull(product)
            .Validate();

        _ = new Product(
            product.Nome!,
            product.Descricao!,
            product.Tipo!,
            product.Preco!);

        var exist = await repo.Get(product.Id);

        if (exist == null)
            throw new NotFoundException(product.Id ?? "product");

        return await repo.Update(product);
    }

}
