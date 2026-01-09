using FluentAssertions;
using Moq;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.UseCases;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.Test;

public class ProductCreateUseCaseTest : TestBase
{

    [Fact]
    public async Task ProductCreateUseCase_Ok()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var useCase = new ProductCreateUseCase(repo.Object);
        var product = new ProductDto
        {
            Id = GetGuid(),
            Nome = faker.Commerce.ProductName(),
            Descricao = faker.Commerce.ProductDescription(),
            Tipo = faker.Commerce.Categories(1).First(),
            Preco = faker.Random.Decimal(10, 100)
        };
        repo.Setup(r => r.Create(product)).ReturnsAsync(product.Id!);
        
        // Act
        var result = await useCase.Create(product);

        // Assert
        result.Should().Be(product.Id);
    }

    [Fact]
    public async Task ProductCreateUseCase_Null_Product()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var useCase = new ProductCreateUseCase(repo.Object);
        var product = (ProductDto?)null;

        // Act
        var act = () => useCase.Create(product!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

}
