using Microsoft.Extensions.Configuration;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.ExternalServices;
using Restaurant.Order.Application.Interfaces.WebApi;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Order.ExternalServices.Services;

public class PaymentService(
    ISecurityService security,
    IConfiguration configuration) : IPaymentService
{

    private readonly string baseUrl = configuration["ExternalServices:Payment"]
        ?? throw new ArgumentNullException("ExternalServices:Payment");


    public async Task Pay(OrderInfoDto order)
    {
        using var http = new HttpClient();
        var message = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/order/pay");

        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", security.Token);
        message.Content = JsonContent.Create(order);

        var response = await http.SendAsync(message);

        response.EnsureSuccessStatusCode();
    }

}
