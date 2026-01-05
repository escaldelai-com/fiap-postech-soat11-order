using FluentAssertions;
using Moq;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Application.UseCases;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.Test.UseCases;

public class OrderInfoConfirmPayUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderInfoConfirmPayUseCase_Ok()
    {
        // Arrange
        var (orderGet, orderCreate, _, useCase) = GetMocks();
        var order = GetOrder(3, OrderStatus.WaitingPayment);
        orderGet.Setup(x => x.Get(order.Id!)).ReturnsAsync(order);
        orderCreate.Setup(x => x.Create(order)).Returns(GetOrder(order));

        // Act
        var result = await useCase.ConfirmPay(order.Id);

        // Assert
        result.Status.Should().Be(OrderStatus.Paid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task OrderInfoConfirmPayUseCase_Invalid_Order_Id(string? orderId)
    {
        // Arrange
        var (_, _, _, useCase) = GetMocks();

        // Act
        var act = () => useCase.ConfirmPay(orderId);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }



    private (Mock<IOrderGetService>, Mock<IOrderCreateService>, Mock<IOrderRepository>, OrderInfoConfirmPayUseCase) GetMocks()
    {
        var orderGet = new Mock<IOrderGetService>();
        var orderCreate = new Mock<IOrderCreateService>();
        var repo = new Mock<IOrderRepository>();
        var useCase = new OrderInfoConfirmPayUseCase(orderGet.Object, orderCreate.Object, repo.Object);

        return (orderGet, orderCreate, repo, useCase);
    }

}
