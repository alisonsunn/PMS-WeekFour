using System;

namespace PaintManagementSystem.API.Services;

using PaintManagementSystem.API.Repositories;
using PaintManagementSystem.API.Services.Interfaces;
using PaintManagementSystem.API.DTOs;
using PaintManagementSystem.Models.Models;

public class PaintProductsService : IPaintProductsService
{
    private readonly PaintProductRepository _paintProductRepository;

    public PaintProductsService(PaintProductRepository paintProductRepository)
    {
        _paintProductRepository = paintProductRepository;
    }

    private static PaintReturnDTO ReturnDTO(PaintProduct paint)
    {
        return new PaintReturnDTO
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
    }

    public List<PaintReturnDTO> GetAllPaintProducts()
    {
        var paints = _paintProductRepository.GetAllPaintProducts();

        var returnedpaints = paints.Select(paint => new PaintReturnDTO
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

        return returnedpaints;
    }

    // Get paintProduct By ProductId
    public PaintReturnDTO? GetPaintProductById(int id)
    {
        var paint = _paintProductRepository.GetPaintProductById(id);

        if (paint == null)
        {
            return null;
        }

        var paintProduct = ReturnDTO(paint);

        return paintProduct;
    }

    // Update PaintProduct
    public PaintReturnDTO? UpdatePaintProduct(int id, PaintDTO paintDTO)
    {
        var paint = _paintProductRepository.GetPaintProductForUpdate(id);
        if (paint is null)
        {
            return null;
        }

        paint.Name = paintDTO.Name;
        paint.Type = paintDTO.Type;
        paint.Specification = paintDTO.Specification;
        paint.Price = paintDTO.Price;
        paint.BrandId = paintDTO.BrandId;

        _paintProductRepository.SaveChanges();

        var returnedpaint = _paintProductRepository.GetPaintProductById(id);
        if (returnedpaint == null)
        {
            return null;
        }

        var paintProduct = ReturnDTO(paint);

        return paintProduct;
    }

    // Create paintProduct
    public PaintReturnDTO? CreatePaintProduct(PaintDTO paintDTO)
    {
        PaintProduct newPaintProduct = new PaintProduct
        {
            Name = paintDTO.Name,
            Type = paintDTO.Type,
            Specification = paintDTO.Specification,
            Price = paintDTO.Price,
            BrandId = paintDTO.BrandId
        };

        _paintProductRepository.AddPaintProduct(newPaintProduct);
        
        _paintProductRepository.SaveChanges();

        var paint = _paintProductRepository.GetPaintProductById(newPaintProduct.ProductId);

        if (paint is null)
        {
            return null;
        }

        var paintProduct = ReturnDTO(paint);

        return paintProduct;
    }

    // Remove paintProduct
    public PaintReturnDTO? DeletePaintProduct(int id)
    {
        var paint = _paintProductRepository.GetPaintProductById(id);
        if (paint is null)
        {
            return null;
        }
        _paintProductRepository.DeletePaintProduct(paint);

        _paintProductRepository.SaveChanges();

        var paintProduct = ReturnDTO(paint);

        return paintProduct;
    }
}
