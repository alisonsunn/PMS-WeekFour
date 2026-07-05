using System;
using Microsoft.AspNetCore.Mvc;
using PaintManagementSystem.API.DataBase;
using PaintManagementSystem.API.DTOs;
using PaintManagementSystem.Models.Models;


namespace PaintManagementSystem.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class PaintsController : ControllerBase
    {
        private readonly PaintDbContext _context;

        public PaintsController(PaintDbContext paintDbContext)
        {
            _context = paintDbContext;
        }

        // get all paintProducts
        [HttpGet]
        public IActionResult GetAllPaintProducts()
        {
            return Ok(_context.PaintProducts);
        } 

        // get paintProduct By ProductId
        [HttpGet("{id}")]
        public IActionResult GetPaintProductById(int id)
        {
            var result = _context.PaintProducts.FirstOrDefault(paintProduct => paintProduct.ProductId == id);
            return Ok(result);
        }

        // update paintProduct By ProductId
        [HttpPut("{id}")]
        public IActionResult UpdatePaintProduct(int id, [FromBody] PaintDTO paintDTO)
        {
            var paint = _context.PaintProducts.FirstOrDefault(paintProduct => paintProduct.ProductId == id);
            paint.Name = paintDTO.Name;
            paint.Type = paintDTO.Type;
            paint.Specification = paintDTO.Specification;
            paint.Price = paintDTO.Price;
            paint.BrandId = paintDTO.BrandId;

            _context.SaveChanges();
            return Ok(paintDTO);
        }

        // Create paint product
        [HttpPost]
        public IActionResult CreatePaintProduct([FromBody] PaintDTO paintDTO)
        {
            PaintProduct newPaintProduct = new PaintProduct
            {
                Name = paintDTO.Name,
                Type = paintDTO.Type,
                Specification = paintDTO.Specification,
                Price = paintDTO.Price,
                BrandId = paintDTO.BrandId
            };

            _context.PaintProducts.Add(newPaintProduct);
            _context.SaveChanges();
            return Ok(paintDTO);
        }

        // Delete paintProduct by ID
        [HttpDelete("{id}")]
        public IActionResult DeletePaintProduct(int id)
        {
            PaintProduct paintProduct = _context.PaintProducts.First(paintProduct => paintProduct.ProductId == id);
            _context.PaintProducts.Remove(paintProduct);
            _context.SaveChanges();
            return Ok(paintProduct);
        }

        // Filter paintProduct
    }
