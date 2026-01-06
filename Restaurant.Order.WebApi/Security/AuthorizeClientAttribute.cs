using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Order.WebApi.Security;

public class AuthorizeClientAttribute : AuthorizeAttribute
{

    public AuthorizeClientAttribute() : base("client") { }

}
