using Microsoft.AspNetCore.Mvc;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Facade;
using Restaurant.Order.WebApi.Security;

namespace Restaurant.Order.WebApi.Controllers;

[Route("[controller]")]
public class OrderController(
    IOrderFacade facade) : Controller
{

    [HttpGet("waiting")]
    [AuthorizeAdmin]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderInfoDto>))]
    public async Task<IActionResult> GetWaiting()
    {
        var result = await facade.GetWaiting();

        return Ok(result);
    }

    [HttpPost]
    [AuthorizeClient]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody]ClientDto client)
    {
        var result = !string.IsNullOrWhiteSpace(client.Id) 
            ? await facade.CreateById(client.Id)
            : await facade.CreateByCpf(client.CPF!);

        return Ok(result);
    }

    [HttpPost("items")]
    [AuthorizeClient]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddItem([FromBody] OrderInfoRequestDto req)
    {
        var result = await facade.AddItem(req.OrderId, req.ItemId);

        return Ok(result);
    }

    [HttpPost("confirm")]
    [AuthorizeClient]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Confirm([FromBody] OrderInfoRequestDto req)
    {
        var result = await facade.Confirm(req.OrderId);

        return Ok(result);
    }

    [HttpPost("cancel")]
    [AuthorizeClient]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel([FromBody] OrderInfoRequestDto req)
    {
        var result = await facade.Cancel(req.OrderId);

        return Ok(result);
    }


    [HttpPost("pay")]
    [AuthorizePayment]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmPayment([FromBody] OrderInfoRequestDto req)
    {
        var result = await facade.ConfirmPay(req.OrderId);

        return Ok(result);
    }

}
