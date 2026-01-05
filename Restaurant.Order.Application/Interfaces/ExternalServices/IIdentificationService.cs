using Restaurant.Order.Application.DTO;

namespace Restaurant.Order.Application.Interfaces.ExternalServices;

public interface IIdentificationService
{

    Task<ClientDto?> Get(string cpf);

    Task<ClientDto?> GetById(string id);

    Task<IEnumerable<ClientDto>> Get(IEnumerable<string> ids);

}
