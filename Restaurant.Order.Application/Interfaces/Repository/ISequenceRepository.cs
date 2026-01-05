namespace Restaurant.Order.Application.Interfaces.Repository;

public interface ISequenceRepository
{

    Task<int> Get(string prefix);

}
