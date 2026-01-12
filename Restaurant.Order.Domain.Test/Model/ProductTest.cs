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
            Nome = Faker.Commerce.ProductName(),
            Descricao = Faker.Commerce.ProductDescription(),
            Tipo = Faker.Commerce.Categories(1).First(),
            Preco = Faker.Random.Decimal(10, 100)
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
            descricao: Faker.Commerce.ProductDescription(),
            tipo: Faker.Commerce.Categories(1).First(),
            preco: Faker.Random.Decimal(10, 100)
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
            nome: Faker.Commerce.ProductName(),
            descricao: value!,
            tipo: Faker.Commerce.Categories(1).First(),
            preco: Faker.Random.Decimal(10, 100)
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
            nome: Faker.Commerce.ProductName(),
            descricao: Faker.Commerce.ProductDescription(),
            tipo: value!,
            preco: Faker.Random.Decimal(10, 100)
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
            nome: Faker.Commerce.ProductName(),
            descricao: Faker.Commerce.ProductDescription(),
            tipo: Faker.Commerce.Categories(1).First(),
            preco: value
        );

        // Assert
        act.Should().Throw<ValidationException>();
    }

}
