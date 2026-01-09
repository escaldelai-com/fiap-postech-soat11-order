using AutoMapper;
using MongoDB.Driver;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Data.Model;
using SharpCompress.Common;

namespace Restaurant.Order.Data.Repositories;

public class ProductRepository(
    IMapper mapper,
    IMongoDatabase context) : IProductRepository
{

    private readonly IMongoCollection<ProductData> collection = 
        context.GetCollection<ProductData>("product");


    public async Task<ProductDto?> Get(string? id)
    {
        var data = await collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();

        return mapper.Map<ProductDto?>(data);
    }

    public async Task<IEnumerable<ProductDto>> GetByType(string type)
    {
        var data = await collection
            .Find(x => x.Tipo!.ToLower() == type.ToLower())
            .ToListAsync();

        return mapper.Map<IEnumerable<ProductDto>>(data);
    }

    public async Task<string?> Create(ProductDto product)
    {
        var data = mapper.Map<ProductData>(product);

        await collection.InsertOneAsync(data);

        return data.Id!;
    }

    public async Task<string?> Update(ProductDto product)
    {
        var data = mapper.Map<ProductData>(product);

        await collection
            .ReplaceOneAsync(
                filter: x => x.Id == data.Id,
                replacement: data!);

        return data.Id;
    }

    public async Task Delete(string id)
    {
        await collection.DeleteOneAsync(
            filter: x => x.Id == id);
    }

}
