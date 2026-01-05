using FluentAssertions;
using Moq;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Services;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.Test;

public class OrderGetServiceTest : TestBase
{

    [Fact]
    public async Task OrderGetService_Ok()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var service = new OrderGetService(repo.Object);
        var orderId = GetGuid();
        var order = new OrderInfoDto { Id = orderId };
        repo.Setup(x => x.Get(orderId)).ReturnsAsync(order);

        // Act
        var result = await service.Get(orderId);

        // Assert
        result.Should().BeEquivalentTo(order);
    }

    [Fact]
    public async Task OrderGetService_Not_Found()
    {
        // Arrange
        var repo = new Mock<IOrderRepository>();
        var service = new OrderGetService(repo.Object);
        var orderId = GetGuid();
        repo.Setup(x => x.Get(orderId)).ReturnsAsync(() => null);

        // Act
        var act = () => service.Get(orderId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

}
