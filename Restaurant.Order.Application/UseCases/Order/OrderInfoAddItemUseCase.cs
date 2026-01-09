using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Application.Interfaces.UseCases;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.UseCases;

public class OrderInfoAddItemUseCase(
    IOrderGetService orderGet,
    IOrderItemGetService itemGet,
    IOrderCreateService orderCreate,
    IOrderRepository repo) : IOrderInfoAddItemUseCase
{

    public async Task<OrderInfoDto> AddItem(string? orderId, string? itemId)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(orderId)
            .IsNotNullOrWhiteSpace(itemId)
            .Validate();

        var order = await orderGet.Get(orderId!);

        if (order.Status != OrderStatus.Elaboration)
            throw new OrderStatusException(order.Status!, "add item");

        order.Items.Add(await itemGet.Get(itemId!));

        var model = orderCreate.Create(order);

        await repo.Update(order);

        return order;
    }



}
