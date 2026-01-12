using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Application.Interfaces.UseCases;
using Restaurant.Order.Domain;

namespace Restaurant.Order.Application.UseCases;

public class OrderInfoCancelUseCase(
    IOrderGetService orderGet,
    IOrderCreateService orderCreate,
    IOrderRepository repo) : IOrderInfoCancelUseCase
{

    public async Task<OrderInfoDto> Cancel(string? orderId)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(orderId)
            .Validate();

        var order = await orderGet.GetById(orderId!);
        var model = orderCreate.Create(order);

        model.Cancel();
        order.Status = model.Status;

        await repo.Update(order);

        return order;
    }

}
