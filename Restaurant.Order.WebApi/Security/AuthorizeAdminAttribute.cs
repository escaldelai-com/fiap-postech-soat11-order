using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Order.WebApi.Security;

public class AuthorizeAdminAttribute : AuthorizeAttribute
{

    public AuthorizeAdminAttribute() : base("admin") { }

}
