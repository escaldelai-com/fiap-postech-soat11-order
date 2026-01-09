using AutoMapper;
using MongoDB.Driver;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Data.Model;

namespace Restaurant.Order.Data.Repositories;

public class ProductTypeRepository(
    IMapper mapper,
    IMongoDatabase context) : IProductTypeRepository
{

    private readonly IMongoCollection<ProductTypeData> collection =
        context.GetCollection<ProductTypeData>("product-type");


    public async Task<IEnumerable<ProductTypeDto>> GetList()
    {
        var data = await collection
            .Find(_ => true)
            .ToListAsync();

        return mapper.Map<IEnumerable<ProductTypeDto>>(data);
    }

}
