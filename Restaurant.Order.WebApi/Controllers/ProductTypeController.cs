using Microsoft.AspNetCore.Mvc;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Facade;
using Restaurant.Order.WebApi.Security;

namespace Restaurant.Order.WebApi.Controllers;

[Route("product/type")]
public class ProductTypeController(
    IProductTypeFacade facade) : Controller
{

    [HttpGet]
    [AuthorizeAdmin]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductTypeDto>))]
    public async Task<IActionResult> GetList()
    {
        var result = await facade.GetList();

        return Ok(result);
    }

}
