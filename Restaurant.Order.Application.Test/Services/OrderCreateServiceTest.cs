using FluentAssertions;
using Restaurant.Order.Application.Services;

namespace Restaurant.Order.Application.Test;

public class OrderCreateServiceTest : TestBase
{

    [Fact]
    public void OrderCreateService_Ok()
    {
        // Arrange
        var order = GetOrder(3);
        var service = new OrderCreateService();

        // Act
        var result = service.Create(order);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(3);
    }

}
