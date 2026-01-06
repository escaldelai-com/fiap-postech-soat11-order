using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Order.WebApi.Security;

public class AuthorizePaymentAttribute : AuthorizeAttribute
{

    public AuthorizePaymentAttribute() : base("payment") { }

}
