using FluentAssertions;

namespace Restaurant.Order.Model.Test;

public class OrderItemTest : TestBase
{

    [Fact]
    public void OrderItem_Ok()
    {
        // Arrange
        var item = new
        {
            Nome = faker.Commerce.ProductName(),
            Tipo = faker.Commerce.Categories(1).First(),
            Preco = faker.Random.Decimal(10, 100)
        };

        // Act
        var test = new OrderItem(
            item.Nome,
            item.Tipo,
            item.Preco
        );

        // Assert
        test.Should().BeEquivalentTo(item);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void OrderItem_Invalid_Name(string? value)
    {
        // Act
        var act = () => new OrderItem(
            value!,
            faker.Commerce.Categories(1).First(),
            faker.Random.Decimal(10, 100)
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void OrderItem_Invalid_Type(string? value)
    {
        // Act
        var act = () => new OrderItem(
            faker.Commerce.ProductName(),
            value!,
            faker.Random.Decimal(10, 100)
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void OrderItem_Invalid_Price(decimal value)
    {
        // Act
        var act = () => new OrderItem(
            faker.Commerce.ProductName(),
            faker.Commerce.Categories(1).First(),
            value
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void OrderItem_CompareTo_Equals()
    {
        // Arrange
        var item = new
        {
            Nome = faker.Commerce.ProductName(),
            Tipo = faker.Commerce.Categories(1).First(),
            Preco = faker.Random.Decimal(10, 100)
        };
        var orderItem1 = new OrderItem(item.Nome, item.Tipo, item.Preco);
        var orderItem2 = new OrderItem(item.Nome, item.Tipo, item.Preco);

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(0);
    }

    [Fact]
    public void OrderItem_CompareTo_Null()
    {
        // Arrange
        var orderItem1 = new OrderItem("A", "A", 1m);
        OrderItem? orderItem2 = null;

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(1);
    }

    [Fact]
    public void OrderItem_CompareTo_Type_Greater()
    {
        // Arrange
        var orderItem1 = new OrderItem("A", "B", 1m);
        var orderItem2 = new OrderItem("A", "A", 2m);

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(1);
    }

    [Fact]
    public void OrderItem_CompareTo_Type_Less()
    {
        // Arrange
        var orderItem1 = new OrderItem("A", "A", 1m);
        var orderItem2 = new OrderItem("A", "B", 2m);

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(-1);
    }

    [Fact]
    public void OrderItem_CompareTo_Name_Greater()
    {
        // Arrange
        var orderItem1 = new OrderItem("B", "A", 1m);
        var orderItem2 = new OrderItem("A", "A", 2m);

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(1);
    }

    [Fact]
    public void OrderItem_CompareTo_Name_Less()
    {
        // Arrange
        var orderItem1 = new OrderItem("A", "A", 1m);
        var orderItem2 = new OrderItem("B", "A", 2m);

        // Act
        var compareResult = orderItem1.CompareTo(orderItem2);

        // Assert
        compareResult.Should().Be(-1);
    }

}
