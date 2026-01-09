using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.ExternalServices;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.Interfaces.UseCases;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.UseCases;

public class OrderInfoCreateUseCase(
    IIdentificationService idService,
    IOrderRepository repo,
    ISequenceRepository seq) : IOrderInfoCreateUseCase
{

    public async Task<OrderInfoDto> CreateByCpf(string cpf)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(cpf)
            .Validate();

        var client = await idService.Get(cpf);

        if (client == null)
            throw new NotFoundException(cpf);

        var number = await seq.Get("order");
        var model = new OrderInfo(
            DateTime.Now, number, client.Id!, OrderStatus.Elaboration);

        var data = new OrderInfoDto
        {
            Cliente = client,
            Data = model.Data,
            Numero = model.Numero,
            Status = model.Status
        };

        data.Id = await repo.Create(data);

        return data;
    }

    public async Task<OrderInfoDto> CreateById(string? clientId)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(clientId)
            .Validate();

        var number = await seq.Get("order");
        var model = new OrderInfo(
            DateTime.Now, number, clientId!, OrderStatus.Elaboration);

        var client = await idService.GetById(clientId);

        if (client == null)
            throw new NotFoundException(clientId!);

        var data = new OrderInfoDto
        {
            Cliente = client,
            Data = model.Data,
            Numero = model.Numero,
            Status = model.Status
        };

        data.Id = await repo.Create(data);

        return data;
    }

}
