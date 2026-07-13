using System;

namespace PaintManagementSystem.API.Repositories;
using PaintManagementSystem.API.DataBase;
using PaintManagementSystem.Models.Models;
using Microsoft.EntityFrameworkCore;

public class PaintProductRepository
{
    private readonly PaintDbContext _context;

    public PaintProductRepository(PaintDbContext paintDbContext)
    {
        _context = paintDbContext;
    }

    // Get All paints
    public List<PaintProduct> GetAllPaintProducts()
    {
        return _context.PaintProducts.Include(paintProduct => paintProduct.PaintBrand).ToList();
    }

    // Get paintProduct By ProductId
    public PaintProduct? GetPaintProductById(int id)
    {
        return _context.PaintProducts.Include(paint => paint.PaintBrand).FirstOrDefault(paintProduct => paintProduct.ProductId == id);
    }

    // Update paintPrduct By ProductId - Find the updated product
    public PaintProduct? GetPaintProductForUpdate(int id)
    {
        return  _context.PaintProducts.Include(paint => paint.PaintBrand).FirstOrDefault(paintProduct => paintProduct.ProductId == id);
    }

    // Create new Paint into database
    public void AddPaintProduct(PaintProduct paintProduct)
    {
        _context.PaintProducts.Add(paintProduct);
    }

    // Delete paint from database
    public void DeletePaintProduct(PaintProduct paintProduct)
    {
        _context.PaintProducts.Remove(paintProduct);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
