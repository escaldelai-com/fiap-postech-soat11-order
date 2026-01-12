using Bogus;
using Bogus.Extensions.Brazil;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.Test;

public abstract class TestBase
{
    protected Faker Faker { get; } = new("pt_BR");

    protected static string GetGuid()
    {
        return Guid.NewGuid().ToString("n");
    }

    protected OrderInfoDto GetOrder(int items = 3, string? status = null)
    {
        return new OrderInfoDto
        {
            Id = GetGuid(),
            Data = Faker.Date.Past(),
            Numero = Faker.Random.Int(1, 9999),
            Cliente = new ClientDto
            {
                Id = GetGuid(),
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CPF = Faker.Person.Cpf()
            },
            Status = status ?? Faker.Random.Word(),
            Items = [.. Faker.Make(items, GetOrderItem)]
        };
    }

    protected static OrderInfo GetOrder(OrderInfoDto order)
    {
        var model = new OrderInfo(
            order.Data!.Value,
            order.Numero!.Value,
            order.Cliente!.Id!,
            order.Status!);

        foreach (var item in order.Items)
        {
            model.AddItem(new OrderItem(
                item.Nome!,
                item.Tipo!,
                item.Preco!.Value));
        }

        return model;
    }

    protected OrderItemDto GetOrderItem()
    {
        return new OrderItemDto
        {
            Id = GetGuid(),
            Nome = Faker.Commerce.ProductName(),
            Tipo = Faker.Commerce.Categories(1).First(),
            Preco = Faker.Random.Decimal(10, 100)
        };
    }

}
