using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.UseCases;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.UseCases;

public class ProductCreateUseCase(
    IProductRepository repo) : IProductCreateUseCase
{

    public async Task<string?> Create(ProductDto product)
    {
        Validator.Create()
            .IsNotNull(product)
            .Validate();

        _ = new Product(
            product.Nome!,
            product.Descricao!,
            product.Tipo!,
            product.Preco!);

        return await repo.Create(product);
    }

}
