using System;
using Microsoft.AspNetCore.Mvc;
using PaintManagementSystem.API.Services.Interfaces;
using PaintManagementSystem.API.DTOs;
using PaintManagementSystem.API.Services;
using PaintManagementSystem.Models.Models;


namespace PaintManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaintProductsController : ControllerBase
{
    private readonly IPaintProductsService _paintProductsService;

    public PaintProductsController(IPaintProductsService paintProductsService)
    {
        _paintProductsService = paintProductsService;
    }

    // get all paintProducts
    [HttpGet]
    public IActionResult GetAllPaintProducts()
    {
        var result = _paintProductsService.GetAllPaintProducts();
        return Ok(result);
    }

    // get paintProduct By ProductId
    [HttpGet("{id}")]
    public IActionResult GetPaintProductById(int id)
    {
        var result = _paintProductsService.GetPaintProductById(id);
        if (result is null)
        {
            return NotFound($"Paint Not Found");
        }
        return Ok(result);
    }

    // update paintProduct By ProductId
    [HttpPut("{id}")]
    public IActionResult UpdatePaintProduct(int id, [FromBody] PaintDTO paintDTO)
    {
        var result = _paintProductsService.UpdatePaintProduct(id, paintDTO);
        if (result == null)
        {
            return NotFound("Paint Product Not Found");
        }
        return Ok(result);
    }

    // Create paint product
    [HttpPost]
    public IActionResult CreatePaintProduct([FromBody] PaintDTO paintDTO)
    {
        var paint = _paintProductsService.CreatePaintProduct(paintDTO);
        if (paint is null)
        {
            return NotFound("Paint Product Not Found");
        }
        return Ok(paint);
    }

    // Delete paintProduct by ID
    [HttpDelete("{id}")]
    public IActionResult DeletePaintProduct(int id)
    {    
        var paint = _paintProductsService.DeletePaintProduct(id);
        if (paint is null)
        {
            return NotFound("Paint Product Not Found");
        }
        
        return Ok(paint);
    }
}
