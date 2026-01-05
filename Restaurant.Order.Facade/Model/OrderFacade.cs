using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.ExternalServices;
using Restaurant.Order.Application.Interfaces.Facade;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.UseCases;
using Restaurant.Order.Model;

namespace Restaurant.Order.Facade;

public class OrderFacade(
    IIdentificationService idService,
    IPaymentService payService,
    IPreparationService prepService,
    IOrderRepository repo,
    IOrderInfoCreateUseCase createUseCase,
    IOrderInfoAddItemUseCase addItemUseCase,
    IOrderInfoConfirmUseCase confirmUseCase,
    IOrderInfoCancelUseCase cancelUseCase,
    IOrderInfoConfirmPayUseCase payUseCase) : IOrderFacade
{

    public async Task<IEnumerable<OrderInfoDto>> GetWaiting()
    {
        var orders = await repo.GetListByStatuses(
            OrderStatus.Paid,
            OrderStatus.Received,
            OrderStatus.Preparing,
            OrderStatus.Delivery);

        if (!orders.Any())
            return orders;

        var dic = await GetClients(orders);

        foreach (var order in orders)
        {
            if (dic.TryGetValue(order.Cliente!.Id!, out var client))
                order.Cliente = client;
        }

        return orders;
    }

    public async Task<OrderInfoDto> Create(string cpf)
    {
        return await createUseCase.Create(cpf);
    }

    public async Task<OrderInfoDto> AddItem(string? orderId, string? itemId)
    {
        var order = await addItemUseCase.AddItem(orderId, itemId);
        
        order.Cliente = await idService.GetById(order.Cliente!.Id!);

        return order;
    }

    public async Task<OrderInfoDto> Confirm(string? orderId)
    {
        var order = await confirmUseCase.Confirm(orderId);

        order.Cliente = await idService.GetById(order.Cliente!.Id!);

        await payService.Pay(order);

        return order;
    }

    public async Task<OrderInfoDto> Cancel(string? orderId)
    {
        var order = await cancelUseCase.Cancel(orderId);

        order.Cliente = await idService.GetById(order.Cliente!.Id!);

        return order;
    }

    public async Task<OrderInfoDto> ConfirmPay(string? orderId)
    {
        var order = await payUseCase.ConfirmPay(orderId);

        order.Cliente = await idService.GetById(order.Cliente!.Id!);

        await prepService.Confirm(order);

        return order;
    }


    private async Task<Dictionary<string, ClientDto>> GetClients(IEnumerable<OrderInfoDto> orders)
    {
        var ids = orders.Select(o => o.Cliente!.Id).Cast<string>();
        var data = await idService.Get(ids);

        return data.ToDictionary(c => c.Id!);
    }

}
