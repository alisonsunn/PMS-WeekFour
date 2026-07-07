using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintManagementSystem.API.DataBase;
using PaintManagementSystem.API.DTOs;
using PaintManagementSystem.Models.Models;


namespace PaintManagementSystem.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class PaintProductsController : ControllerBase
    {
        private readonly PaintDbContext _context;

        public PaintProductsController(PaintDbContext paintDbContext)
        {
            _context = paintDbContext;
        }

        // get all paintProducts
        [HttpGet]
        public IActionResult GetAllPaintProducts()
        {
            var paints = _context.PaintProducts.Select(paint => new PaintReturnDTO
            {
                Name = paint.Name,
                Type = paint.Type,
                Specification = new PaintSpecificationDTO
                {
                    Colour = paint.Specification.Colour,
                    SizeInLiters = paint.Specification.SizeInLiters
                },
                Price = paint.Price,
                BrandId = paint.BrandId,
                BrandName = paint.PaintBrand.Name
            }).ToList();

            return Ok(paints);
        } 

        // get paintProduct By ProductId
        [HttpGet("{id}")]
        public IActionResult GetPaintProductById(int id)
        {
            var paint = _context.PaintProducts.Include(paint => paint.PaintBrand).FirstOrDefault(paintProduct => paintProduct.ProductId == id);

            if (paint is null)
            {
                return NotFound($"Paint Product Not Found");
            }

            var paintProduct = new PaintReturnDTO
            {
                Name = paint.Name,
                Type = paint.Type,
                Specification = new PaintSpecificationDTO
                {
                    Colour = paint.Specification.Colour,
                    SizeInLiters = paint.Specification.SizeInLiters
                },
                Price = paint.Price,
                BrandId = paint.BrandId,
                BrandName = paint.PaintBrand.Name
            };

            return Ok(paintProduct);
        }

        // update paintProduct By ProductId
        [HttpPut("{id}")]
        public IActionResult UpdatePaintProduct(int id, [FromBody] PaintDTO paintDTO)
        {
            var paint = _context.PaintProducts.Include(paint => paint.PaintBrand).FirstOrDefault(paintProduct => paintProduct.ProductId == id);

            if (paint is null)
            {
                return NotFound($"Paint Product Not Found");
            }

            paint.Name = paintDTO.Name;
            paint.Type = paintDTO.Type;
            paint.Specification = paintDTO.Specification;
            paint.Price = paintDTO.Price;
            paint.BrandId = paintDTO.BrandId;

            _context.SaveChanges();

            var updatedPaint = new PaintReturnDTO
            {
                Name = paint.Name,
                Type = paint.Type,
                Specification = new PaintSpecificationDTO
                {
                    Colour = paint.Specification.Colour,
                    SizeInLiters = paint.Specification.SizeInLiters
                },
                Price = paint.Price,
                BrandId = paint.BrandId,
                BrandName = paint.PaintBrand.Name
            };
            return Ok(updatedPaint);
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

            var paint = _context.PaintProducts.Include(paint => paint.PaintBrand).First(paintProduct => paintProduct.ProductId == newPaintProduct.ProductId);

            var paintReturnDTO = new PaintReturnDTO
            {
                Name = paint.Name,
                Type = paint.Type,
                Specification = new PaintSpecificationDTO
                {
                    Colour = paint.Specification.Colour,
                    SizeInLiters = paint.Specification.SizeInLiters
                },
                Price = paint.Price,
                BrandId = paint.BrandId,
                BrandName = paint.PaintBrand.Name
            };

            return CreatedAtAction(nameof(GetPaintProductById), new {id = paint.ProductId}, paintReturnDTO);
        }

        // Delete paintProduct by ID
        [HttpDelete("{id}")]
        public IActionResult DeletePaintProduct(int id)
        {
            PaintProduct paint = _context.PaintProducts.First(paintProduct => paintProduct.ProductId == id);

            _context.PaintProducts.Remove(paint);
            _context.SaveChanges();

            var paintReturnDTO = new PaintReturnDTO
            {
                Name = paint.Name,
                Type = paint.Type,
                Specification = new PaintSpecificationDTO
                {
                    Colour = paint.Specification.Colour,
                    SizeInLiters = paint.Specification.SizeInLiters
                },
                Price = paint.Price,
                BrandId = paint.BrandId,
                BrandName = paint.PaintBrand.Name
            };
            return Ok(paintReturnDTO);
        }
    }
