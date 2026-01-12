using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.Facade;
using Restaurant.Order.Domain;
using Restaurant.Order.WebApi.Security;

namespace Restaurant.Order.WebApi.Controllers;

[Route("[controller]")]
public class ProductController(
    IProductFacade facade) : Controller
{

    [HttpGet("{id}")]
    [Authorize(Claims.Product.Get)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
    public async Task<IActionResult> Get(string? id)
    {
        var data = await facade.GetById(id);

        return data != null
            ? Ok(data)
            : NotFound();
    }

    [HttpGet("list/{type}")]
    [Authorize(Claims.Product.GetByType)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
    public async Task<IActionResult> GetByType(string type)
    {
        var data = await facade.GetByType(type);

        return data != null
            ? Ok(data)
            : NotFound();
    }

    [HttpPost]
    [Authorize(Claims.Product.Create)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<IActionResult> Create([FromBody] ProductDto product)
    {
        var id = await facade.Create(product);

        return Ok(id);
    }

    [HttpPut]
    [Authorize(Claims.Product.Update)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] ProductDto product)
    {
        try
        {
            var id = await facade.Update(product);

            return Ok(id);
        }
        catch (NotFoundException)
        {
            return BadRequest();
        }
        catch
        {
            throw;
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Claims.Product.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await facade.Delete(id);

            return NoContent();
        }
        catch (NotFoundException)
        {
            return BadRequest();
        }
        catch
        {
            throw;
        }
    }

}
