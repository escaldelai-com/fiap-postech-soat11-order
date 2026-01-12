using AutoMapper;
using MongoDB.Driver;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Data.Model;

namespace Restaurant.Order.Data.Repositories;


// Cultureinfo não fará diferença para o driver do MongoDB podendo inclusive causar erros na execução
#pragma warning disable CA1304
#pragma warning disable CA1862
#pragma warning disable CA1311 

public class ProductRepository(
    IMapper mapper,
    IMongoDatabase context) : IProductRepository
{

    private readonly IMongoCollection<ProductData> collection =
        context.GetCollection<ProductData>("product");


    public async Task<ProductDto?> GetById(string? id)
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

#pragma warning restore CA1311
#pragma warning restore CA1862
#pragma warning restore CA1304
