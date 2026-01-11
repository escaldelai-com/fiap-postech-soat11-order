using FluentAssertions;
using Moq;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Application.UseCases;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.Test;

public class OrderInfoConfirmUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderInfoConfirmUseCase_Ok()
    {
        // Arrange
        var (orderGet, orderCreate, _, useCase) = GetMocks();
        var order = GetOrder(3, OrderStatus.Elaboration);
        orderGet.Setup(x => x.Get(order.Id!)).ReturnsAsync(order);
        orderCreate.Setup(x => x.Create(order)).Returns(GetOrder(order));

        // Act
        var result = await useCase.Confirm(order.Id);

        // Assert
        result.Status.Should().Be(OrderStatus.WaitingPayment);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task OrderInfoConfirmUseCase_Invalid_Order_Id(string? orderId)
    {
        // Arrange
        var (_, _, _, useCase) = GetMocks();

        // Act
        var act = () => useCase.Confirm(orderId);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }



    private (Mock<IOrderGetService>, Mock<IOrderCreateService>, Mock<IOrderRepository>, OrderInfoConfirmUseCase) GetMocks()
    {
        var orderGet = new Mock<IOrderGetService>();
        var orderCreate = new Mock<IOrderCreateService>();
        var repo = new Mock<IOrderRepository>();
        var useCase = new OrderInfoConfirmUseCase(orderGet.Object, orderCreate.Object, repo.Object);

        return (orderGet, orderCreate, repo, useCase);
    }


}
