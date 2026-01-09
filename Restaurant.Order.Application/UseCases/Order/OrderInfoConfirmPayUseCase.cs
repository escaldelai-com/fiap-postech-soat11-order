using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.Services;
using Restaurant.Order.Application.Interfaces.UseCases;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.UseCases;

public class OrderInfoConfirmPayUseCase(
    IOrderGetService orderGet,
    IOrderCreateService orderCreate,
    IOrderRepository repo) : IOrderInfoConfirmPayUseCase
{

    public async Task<OrderInfoDto> ConfirmPay(string? orderId)
    {
        Validator.Create()
           .IsNotNullOrWhiteSpace(orderId)
           .Validate();

        var order = await orderGet.Get(orderId!);
        var model = orderCreate.Create(order);

        model.ConfirmPay();
        order.Status = model.Status;

        await repo.Update(order);

        return order;
    }

}
