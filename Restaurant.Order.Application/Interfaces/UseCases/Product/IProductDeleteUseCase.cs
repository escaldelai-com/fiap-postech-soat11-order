namespace Restaurant.Order.Application.Interfaces.UseCases;

public interface IProductDeleteUseCase
{

    Task Delete(string id);

}
