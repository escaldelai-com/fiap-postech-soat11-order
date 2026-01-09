using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Order.WebApi.Controllers;

public class MainController : Controller
{

    [HttpGet("/")]
    public IActionResult LifeTest()
    {
        return NoContent();
    }

}
