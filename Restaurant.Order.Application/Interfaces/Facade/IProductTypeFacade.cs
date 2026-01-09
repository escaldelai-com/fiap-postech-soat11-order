namespace Restaurant.Order.Application.Interfaces.Facade;

public interface IProductTypeFacade
{

    Task<IEnumerable<string>> GetList();

}
