using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.Services;

public class OrderCreateService : IOrderCreateService
{

    public OrderInfo Create(OrderInfoDto orderDto)
    {
        var order = new OrderInfo(
            orderDto.Data!.Value,
            orderDto.Numero!.Value,
            orderDto.Cliente!.Id!,
            orderDto.Status!);

        foreach (var itemDto in orderDto.Items)
            order.AddItem(Create(itemDto));

        return order;
    }



    private OrderItem Create(OrderItemDto itemDto)
    {
        return new OrderItem(
            itemDto.Nome!,
            itemDto.Tipo!,
            itemDto.Preco!.Value);
    }

}
