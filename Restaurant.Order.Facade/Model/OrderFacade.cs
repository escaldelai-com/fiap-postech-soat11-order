using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.ExternalServices;
using Restaurant.Order.Application.Interfaces.Facade;
using Restaurant.Order.Application.Interfaces.UseCases;

namespace Restaurant.Order.Facade;

public class OrderFacade(
    IIdentificationService idService,
    IPaymentService payService,
    IPreparationService prepService,
    IOrderInfoCreateUseCase createUseCase,
    IOrderInfoAddItemUseCase addItemUseCase,
    IOrderInfoConfirmUseCase confirmUseCase,
    IOrderInfoCancelUseCase cancelUseCase,
    IOrderInfoConfirmPayUseCase payUseCase) : IOrderFacade
{

    public async Task<OrderInfoDto> CreateById(string clientId)
    {
        return await createUseCase.CreateById(clientId);
    }

    public async Task<OrderInfoDto> CreateByCpf(string cpf)
    {
        return await createUseCase.CreateByCpf(cpf);
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

}
