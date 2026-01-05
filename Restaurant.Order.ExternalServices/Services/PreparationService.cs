using Microsoft.Extensions.Configuration;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.ExternalServices;
using System.Net.Http.Json;

namespace Restaurant.Order.ExternalServices.Services;

public class PreparationService(
    IConfiguration configuration) : IPreparationService
{

    private readonly string baseUrl = configuration["ExternalServices:Preparation"]
        ?? throw new ArgumentNullException("ExternalServices:Preparation");


    public async Task Confirm(OrderInfoDto order)
    {
        using var http = new HttpClient();
        var message = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/order/confirm");
        message.Content = JsonContent.Create(order);
        var response = await http.SendAsync(message);

        response.EnsureSuccessStatusCode();
    }

}
