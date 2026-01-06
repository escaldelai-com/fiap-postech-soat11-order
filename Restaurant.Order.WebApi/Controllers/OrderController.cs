using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Facade;
using Restaurant.Order.WebApi.Security;

namespace Restaurant.Order.WebApi.Controllers;

[Route("[controller]")]
public class OrderController(
    IOrderFacade facade) : Controller
{

    [HttpPost]
    [Authorize(Claims.Order.Create)]
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
    [Authorize(Claims.Order.AddItem)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddItem([FromBody] OrderInfoRequestDto req)
    {
        var result = await facade.AddItem(req.OrderId, req.ItemId);

        return Ok(result);
    }

    [HttpPost("confirm")]
    [Authorize(Claims.Order.Confirm)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Confirm([FromBody] OrderInfoRequestDto req)
    {
        var result = await facade.Confirm(req.OrderId);

        return Ok(result);
    }

    [HttpPost("cancel")]
    [Authorize(Claims.Order.Cancel)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel([FromBody] OrderInfoRequestDto req)
    {
        var result = await facade.Cancel(req.OrderId);

        return Ok(result);
    }


    [HttpPost("pay")]
    [Authorize(Claims.Order.ConfirmPayment)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmPayment([FromBody] OrderInfoRequestDto req)
    {
        var result = await facade.ConfirmPay(req.OrderId);

        return Ok(result);
    }

}
