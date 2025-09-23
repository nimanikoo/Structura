using MediatR;
using Microsoft.AspNetCore.Mvc;
using Structura.Application.Features.Products.Commands;
using Structura.Application.Features.Products.Queries;

namespace Structura.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    //Its sample collection api only for check work and logic of architecture
    
    /// <summary>
    ///  Add new product
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddProductCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// get product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }
}