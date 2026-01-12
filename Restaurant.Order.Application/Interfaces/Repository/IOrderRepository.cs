using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.Repository;

public interface IOrderRepository
{

    Task<OrderInfoDto?> GetById(string? id);

    Task<IEnumerable<OrderInfoDto>> GetListByStatuses(params string[] statuses);

    Task<string> Create(OrderInfoDto data);

    Task Update(OrderInfoDto data);

}
