using Bogus.Extensions.Brazil;
using FluentAssertions;
using Moq;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Application.UseCases;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.Test;

public class OrderInfoAddItemUseCaseTest : TestBase
{

    [Fact]
    public async Task OrderInfoAddItemUseCase_Ok()
    {
        // Arrange
        var (orderGet, itemGet, orderCreate, repo, useCase) = GetMocks();
        var (orderId, itemId) = (GetGuid(), GetGuid());
        orderGet.Setup(x => x.Get(orderId)).ReturnsAsync(GetOrder(3, OrderStatus.Elaboration));
        itemGet.Setup(x => x.Get(itemId)).ReturnsAsync(GetOrderItem);

        // Act
        var result = await useCase.AddItem(orderId, itemId);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(4);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task OrderInfoAddItemUseCase_Invalid_OrderId(string? value)
    {
        // Arrange
        var (_, _, _, _, useCase) = GetMocks();
        var itemId = GetGuid();

        // Act
        var act = () => useCase.AddItem(value!, itemId);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task OrderInfoAddItemUseCase_Invalid_ItemId(string? value)
    {
        // Arrange
        var (_, _, _, _, useCase) = GetMocks();
        var orderId = GetGuid();

        // Act
        var act = () => useCase.AddItem(orderId, value!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task OrderInfoAddItemUseCase_Invalid_Order_Status()
    {
        // Arrange
        var (orderGet, _, _, _, useCase) = GetMocks();
        var (orderId, itemId) = (GetGuid(), GetGuid());
        orderGet.Setup(x => x.Get(orderId)).ReturnsAsync(GetOrder(3, OrderStatus.Canceled));

        // Act
        var act = () => useCase.AddItem(orderId, itemId);

        // Assert
        await act.Should().ThrowAsync<OrderStatusException>();
    }



    private (Mock<IOrderGetService>, Mock<IOrderItemGetService>, Mock<IOrderCreateService>, Mock<IOrderRepository>, OrderInfoAddItemUseCase) GetMocks()
    {
        var orderGet = new Mock<IOrderGetService>();
        var itemGet = new Mock<IOrderItemGetService>();
        var orderCreate = new Mock<IOrderCreateService>();
        var repo = new Mock<IOrderRepository>();
        var useCase = new OrderInfoAddItemUseCase(orderGet.Object, itemGet.Object, orderCreate.Object, repo.Object);

        return (orderGet, itemGet, orderCreate, repo, useCase);
    }

}
