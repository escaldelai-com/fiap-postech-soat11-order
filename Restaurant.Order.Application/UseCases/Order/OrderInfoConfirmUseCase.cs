using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Application.Interfaces.UseCases;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.UseCases;

public class OrderInfoConfirmUseCase(
    IOrderGetService orderGet,
    IOrderCreateService orderCreate,
    IOrderRepository repo) : IOrderInfoConfirmUseCase
{

    public async Task<OrderInfoDto> Confirm(string? orderId)
    {
        Validator.Create()
           .IsNotNullOrWhiteSpace(orderId)
           .Validate();

        var order = await orderGet.Get(orderId!);
        var model = orderCreate.Create(order);

        model.Confirm();
        order.Status = model.Status;

        await repo.Update(order);

        return order;
    }

}
