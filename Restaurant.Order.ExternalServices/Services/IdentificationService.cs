using Microsoft.Extensions.Configuration;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.ExternalServices;
using Restaurant.Order.Application.Interfaces.Presenter;
using System.Web;

namespace Restaurant.Order.ExternalServices;

public class IdentificationService(
    IJsonPresenter presenter,
    IConfiguration configuration) : IIdentificationService
{

    private readonly string baseUrl = configuration["ExternalServices:Identification"]
        ?? throw new ArgumentNullException("ExternalServices:Identification");


    public async Task<ClientDto?> Get(string cpf)
    {
        using var http = new HttpClient();
        var message = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/client/cpf/{cpf}");
        var response = await http.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return presenter.Deserialize<ClientDto>(content);
    }

    public async Task<ClientDto?> GetById(string id)
    {
        using var http = new HttpClient();
        var message = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/client/id/{id}");
        var response = await http.SendAsync(message);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return presenter.Deserialize<ClientDto>(content);
    }

    public async Task<IEnumerable<ClientDto>> Get(IEnumerable<string> ids)
    {
        using var http = new HttpClient();
        var query = GetQueryIds(ids);
        var message = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/client/list{query}");
        var response = await http.SendAsync(message);

        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        
        return presenter.Deserialize<IEnumerable<ClientDto>>(content) 
            ?? Enumerable.Empty<ClientDto>();
    }



    private string GetQueryIds(IEnumerable<string> ids)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        foreach (var id in ids)
            query.Add("id", id);

        var result = query.ToString();

        return !string.IsNullOrEmpty(result) 
            ? $"?{result}"
            : string.Empty;
    }

}
