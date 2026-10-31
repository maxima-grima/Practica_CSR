using Microsoft.AspNetCore.Mvc;
using Practica_CSR.Models.DTOs.Requests;
using Practica_CSR.Services.Interfaces;

namespace Practica_CSR.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(_service.GetAllProducts());
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _service.GetProductById(id);
        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductForCreateDto dto)
    {
        try
        {
            var created = _service.CreateProduct(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductForUpdateDto dto)
    {
        var product = _service.GetProductById(id);
        if (product == null) return NotFound();

        try
        {
            _service.UpdateProduct(id, dto);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _service.GetProductById(id);
        if (product == null) return NotFound();

        _service.DeleteProduct(id);
        return NoContent();
    }

    [HttpGet("search")]
    public IActionResult SearchProductsByName([FromQuery] string name)
    {
        return Ok(_service.SearchProductsByName(name));
    }

    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        return Ok(_service.GetStats());
    }
}
