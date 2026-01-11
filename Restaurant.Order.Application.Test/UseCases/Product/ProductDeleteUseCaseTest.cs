using FluentAssertions;
using Moq;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.UseCases;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.Test;

public class ProductDeleteUseCaseTest : TestBase
{

    [Fact]
    public async Task ProductDeleteUseCase_Ok()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var useCase = new ProductDeleteUseCase(repo.Object);
        var id = GetGuid();
        repo.Setup(x => x.Get(id)).ReturnsAsync(new ProductDto());

        // Act
        var act = () => useCase.Delete(id);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task ProductDeleteUseCase_Invalid_Id(string? id)
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var useCase = new ProductDeleteUseCase(repo.Object);

        // Act
        var act = () => useCase.Delete(id!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ProductDeleteUseCase_Not_Exists()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var useCase = new ProductDeleteUseCase(repo.Object);
        var id = GetGuid();
        repo.Setup(x => x.Get(id)).ReturnsAsync(() => null);

        // Act
        var act = () => useCase.Delete(id);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

}
