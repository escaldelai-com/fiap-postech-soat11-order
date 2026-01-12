using Restaurant.Order.Application.Interfaces.WebApi;

namespace Restaurant.Order.WebApi.Services;

public class SecurityService(
    IHttpContextAccessor http) : ISecurityService
{

    private const string bearerPrefix = "Bearer ";

    public string Token => GetToken();



    private string GetToken()
    {
        var authorizationHeader =
            $"{http.HttpContext?.Request.Headers.Authorization}";

        return string.IsNullOrWhiteSpace(authorizationHeader)
            ? string.Empty
            : authorizationHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase)
            ? authorizationHeader[bearerPrefix.Length..].Trim()
            : authorizationHeader.Trim();
    }
}
