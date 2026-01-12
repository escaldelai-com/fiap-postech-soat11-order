using FluentAssertions;
using Restaurant.Order.Domain.Exceptions;

namespace Restaurant.Order.Domain.Test;

public class OrderInfoTest : TestBase
{

    [Fact]
    public void OrderInfo_Ok()
    {
        // Arrange
        var data = new
        {
            Data = Faker.Date.Past(),
            Numero = Faker.Random.Int(1, 9999),
            Cliente = GetGuid(),
            Status = Faker.Random.Word(),
            Items = Array.Empty<object>()
        };

        // Act
        var test = new OrderInfo(
            data.Data,
            data.Numero,
            data.Cliente,
            data.Status
        );

        // Assert
        test.Should().BeEquivalentTo(data);
    }

    [Fact]
    public void OrderInfo_Invalid_Date()
    {
        // Act
        var act = () => new OrderInfo(
            Faker.Date.Future(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            Faker.Random.Word()
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void OrderInfo_Invalid_Number(int value)
    {
        // Act
        var act = () => new OrderInfo(
            Faker.Date.Future(),
            value,
            GetGuid(),
            Faker.Random.Word()
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void OrderInfo_Invalid_Client(string? value)
    {
        // Act
        var act = () => new OrderInfo(
            Faker.Date.Future(),
            Faker.Random.Int(1, 9999),
            value!,
            Faker.Random.Word()
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void OrderInfo_Invalid_Status(string? value)
    {
        // Act
        var act = () => new OrderInfo(
            Faker.Date.Future(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            value!
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void OrderInfo_AddItem_Ok()
    {
        // Arrange
        var order = new OrderInfo(
            Faker.Date.Past(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            Faker.Random.Word()
        );
        var item = new OrderItem(
            Faker.Commerce.ProductName(),
            Faker.Commerce.Categories(1).First(),
            Faker.Random.Decimal(10, 100)
        );

        // Act
        order.AddItem(item);

        // Assert
        order.Items.Should()
            .ContainSingle().Which.Should()
            .BeEquivalentTo(item);
    }

    [Fact]
    public void OrderInfo_AddItem_Null()
    {
        // Arrange
        var order = new OrderInfo(
            Faker.Date.Past(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            Faker.Random.Word()
        );

        // Act
        var act = () => order.AddItem(null!);

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void OrderInfo_AddItem_Duplicated()
    {
        // Arrange
        var order = new OrderInfo(
            Faker.Date.Past(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            Faker.Random.Word()
        );
        var item = new OrderItem(
            Faker.Commerce.ProductName(),
            Faker.Commerce.Categories(1).First(),
            Faker.Random.Decimal(10, 100)
        );
        order.AddItem(item);

        // Act
        var act = () => order.AddItem(item);

        // Assert
        act.Should().Throw<DuplicatedException>();
    }

    [Fact]
    public void OrderInfo_Confirm_Ok()
    {
        // Arrange
        var order = new OrderInfo(
            Faker.Date.Past(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Elaboration
        );
        order.AddItem(new OrderItem(
            Faker.Commerce.ProductName(),
            Faker.Commerce.Categories(1).First(),
            Faker.Random.Decimal(10, 100)
        ));

        // Act
        order.Confirm();

        // Assert
        order.Status.Should().Be(OrderStatus.WaitingPayment);
    }

    [Fact]
    public void OrderInfo_Confirm_Invalid_Status()
    {
        // Arrange
        var order = new OrderInfo(
            Faker.Date.Past(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Canceled
        );
        order.AddItem(new OrderItem(
            Faker.Commerce.ProductName(),
            Faker.Commerce.Categories(1).First(),
            Faker.Random.Decimal(10, 100)
        ));

        // Act
        var act = order.Confirm;

        // Assert
        act.Should().Throw<OrderStatusException>();
    }

    [Fact]
    public void OrderInfo_Confirm_With_No_Items()
    {
        // Arrange
        var order = new OrderInfo(
            Faker.Date.Past(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Elaboration
        );

        // Act
        var act = order.Confirm;

        // Assert
        act.Should().Throw<InvalidOrderException>();
    }

    [Fact]
    public void OrderInfo_Cancel_Ok()
    {
        // Arrange
        var order = new OrderInfo(
            Faker.Date.Past(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Elaboration
        );
        order.AddItem(new OrderItem(
            Faker.Commerce.ProductName(),
            Faker.Commerce.Categories(1).First(),
            Faker.Random.Decimal(10, 100)
        ));

        // Act
        order.Cancel();

        // Assert
        order.Status.Should().Be(OrderStatus.Canceled);
    }

    [Fact]
    public void OrderInfo_Cancel_Invalid_Status()
    {
        // Arrange
        var order = new OrderInfo(
            Faker.Date.Past(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Canceled
        );
        order.AddItem(new OrderItem(
            Faker.Commerce.ProductName(),
            Faker.Commerce.Categories(1).First(),
            Faker.Random.Decimal(10, 100)
        ));

        // Act
        var act = order.Cancel;

        // Assert
        act.Should().Throw<OrderStatusException>();
    }

    [Fact]
    public void OrderInfo_Confirm_Pay_Ok()
    {
        // Arrange
        var order = new OrderInfo(
            Faker.Date.Past(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.WaitingPayment
        );
        order.AddItem(new OrderItem(
            Faker.Commerce.ProductName(),
            Faker.Commerce.Categories(1).First(),
            Faker.Random.Decimal(10, 100)
        ));

        // Act
        order.ConfirmPay();

        // Assert
        order.Status.Should().Be(OrderStatus.Paid);
    }

    [Fact]
    public void OrderInfo_Confirm_Pay_Invalid_Status()
    {
        // Arrange
        var order = new OrderInfo(
            Faker.Date.Past(),
            Faker.Random.Int(1, 9999),
            GetGuid(),
            OrderStatus.Canceled
        );
        order.AddItem(new OrderItem(
            Faker.Commerce.ProductName(),
            Faker.Commerce.Categories(1).First(),
            Faker.Random.Decimal(10, 100)
        ));

        // Act
        var act = order.ConfirmPay;

        // Assert
        act.Should().Throw<OrderStatusException>();
    }

}
