using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.ExternalServices;

public interface IPreparationService
{

    Task Confirm(OrderInfoDto order);

}
