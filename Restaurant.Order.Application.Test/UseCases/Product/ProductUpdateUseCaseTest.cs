using FluentAssertions;
using Moq;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.Test;

public class ProductUpdateUseCaseTest : TestBase
{

    [Fact]
    public async Task ProductUpdateUseCase_Ok()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var useCase = new ProductUpdateUseCase(repo.Object);
        var product = new ProductDto
        {
            Id = GetGuid(),
            Nome = faker.Commerce.ProductName(),
            Descricao = faker.Commerce.ProductDescription(),
            Tipo = faker.Commerce.Categories(1).First(),
            Preco = faker.Random.Decimal(10, 100)
        };
        repo.Setup(r => r.Get(product.Id)).ReturnsAsync(product);
        repo.Setup(r => r.Update(product)).ReturnsAsync(product.Id!);

        // Act
        var result = await useCase.Update(product);

        // Assert
        result.Should().Be(product.Id);
    }

    [Fact]
    public async Task ProductUpdateUseCase_Null_Product()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var useCase = new ProductUpdateUseCase(repo.Object);
        var product = (ProductDto?)null;

        // Act
        var act = () => useCase.Update(product!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ProductUpdateUseCase_Not_Exists()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var useCase = new ProductUpdateUseCase(repo.Object);
        var product = new ProductDto
        {
            Id = GetGuid(),
            Nome = faker.Commerce.ProductName(),
            Descricao = faker.Commerce.ProductDescription(),
            Tipo = faker.Commerce.Categories(1).First(),
            Preco = faker.Random.Decimal(10, 100)
        };
        repo.Setup(r => r.Get(product.Id)).ReturnsAsync(() => null);

        // Act
        var act = () => useCase.Update(product!);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

}
