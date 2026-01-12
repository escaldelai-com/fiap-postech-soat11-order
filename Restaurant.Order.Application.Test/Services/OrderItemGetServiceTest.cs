using FluentAssertions;
using Moq;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Services;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.Test;

public class OrderItemGetServiceTest : TestBase
{

    [Fact]
    public async Task OrderItemGetService_Ok()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var service = new OrderItemGetService(repo.Object);
        var itemId = GetGuid();
        var item = new ProductDto { Id = itemId };
        repo.Setup(x => x.GetById(itemId)).ReturnsAsync(item);

        // Act
        var result = await service.GetById(itemId);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task OrderItemGetService_Not_Found()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var service = new OrderItemGetService(repo.Object);
        var itemId = GetGuid();
        repo.Setup(x => x.GetById(itemId)).ReturnsAsync(() => null);

        // Act
        var act = () => service.GetById(itemId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

}
