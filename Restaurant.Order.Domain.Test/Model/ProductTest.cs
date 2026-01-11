using FluentAssertions;

namespace Restaurant.Order.Domain.Test;

public class ProductTest : TestBase
{

    [Fact]
    public void Product_Ok()
    {
        // Arrange
        var testData = new
        {
            Nome = faker.Commerce.ProductName(),
            Descricao = faker.Commerce.ProductDescription(),
            Tipo = faker.Commerce.Categories(1).First(),
            Preco = faker.Random.Decimal(10, 100)
        };

        // Act
        var product = new Product(
            nome: testData.Nome,
            descricao: testData.Descricao,
            tipo: testData.Tipo,
            preco: testData.Preco
        );

        // Assert
        product.Should().BeEquivalentTo(testData);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Product_Invalid_Name(string? value)
    {
        // Act
        var act = () => new Product(
            nome: value!,
            descricao: faker.Commerce.ProductDescription(),
            tipo: faker.Commerce.Categories(1).First(),
            preco: faker.Random.Decimal(10, 100)
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Product_Invalid_Description(string? value)
    {
        // Act
        var act = () => new Product(
            nome: faker.Commerce.ProductName(),
            descricao: value!,
            tipo: faker.Commerce.Categories(1).First(),
            preco: faker.Random.Decimal(10, 100)
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Product_Invalid_Type(string? value)
    {
        // Act
        var act = () => new Product(
            nome: faker.Commerce.ProductName(),
            descricao: faker.Commerce.ProductDescription(),
            tipo: value!,
            preco: faker.Random.Decimal(10, 100)
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Product_Invalid_Price(decimal value)
    {
        // Act
        var act = () => new Product(
            nome: faker.Commerce.ProductName(),
            descricao: faker.Commerce.ProductDescription(),
            tipo: faker.Commerce.Categories(1).First(),
            preco: value
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

}
