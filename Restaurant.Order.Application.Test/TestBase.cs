using Bogus;
using Bogus.Extensions.Brazil;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.Test;

public abstract class TestBase
{

    protected Faker faker = new("pt_BR");

    protected string GetGuid() => Guid.NewGuid().ToString("n");


    protected OrderInfoDto GetOrder(int items = 3, string? status = null)
    {
        return new OrderInfoDto
        {
            Id = GetGuid(),
            Data = faker.Date.Past(),
            Numero = faker.Random.Int(1, 9999),
            Cliente = new ClientDto 
            { 
                Id = GetGuid(),
                Nome = faker.Name.FullName(),
                Email = faker.Internet.Email(),
                CPF = faker.Person.Cpf()
            },
            Status = status ?? faker.Random.Word(),
            Items = faker.Make(items, GetOrderItem).ToList()
        };
    }

    protected OrderInfo GetOrder(OrderInfoDto order)
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
            Nome = faker.Commerce.ProductName(),
            Tipo = faker.Commerce.Categories(1).First(),
            Preco = faker.Random.Decimal(10, 100)
        };
    }

}
