using AutoMapper;
using MongoDB.Driver;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Presenter;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Data.Model;

namespace Restaurant.Order.Data.Repositories;

public class OrderRepository(
    IMapper mapper,
    IDatePresenter presenter,
    IMongoDatabase context) : IOrderRepository
{

    private readonly IMongoCollection<OrderInfoData> collection =
        context.GetCollection<OrderInfoData>("order");


    public async Task<IEnumerable<OrderInfoDto>> GetListByStatuses(params string[] statuses)
    {
        var filter = Builders<OrderInfoData>.Filter
            .In(x => x.Status, statuses);

        var entities = await collection
            .Find(filter)
            .ToListAsync();

        return mapper.Map<IEnumerable<OrderInfoDto>>(entities);
    }

    public async Task<OrderInfoDto?> GetById(string? id)
    {
        var entity = await collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (entity == null)
            return null;

        entity.Data = presenter.ToTimeZone(entity.Data);

        return mapper.Map<OrderInfoDto>(entity);
    }

    public async Task<string> Create(OrderInfoDto data)
    {
        var entity = mapper.Map<OrderInfoData>(data);

        await collection.InsertOneAsync(entity);

        return entity.Id!;
    }

    public async Task Update(OrderInfoDto data)
    {
        var entity = mapper.Map<OrderInfoData>(data);

        await collection.ReplaceOneAsync(
            filter: x => x.Id == entity.Id,
            replacement: entity);
    }

}
