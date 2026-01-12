using Restaurant.Order.Application.Interfaces.Facade;
using Restaurant.Order.Application.Interfaces.Repository;

namespace Restaurant.Order.Facade;

public class ProductTypeFacade(
    IProductTypeRepository repo) : IProductTypeFacade
{

    public async Task<IEnumerable<string>> GetList()
    {
        var data = await repo.GetList();

        return [..
            data.Select(x => x.Nome)
                .Where(x => !string.IsNullOrEmpty(x))
                .Cast<string>()
        ];
    }

}
