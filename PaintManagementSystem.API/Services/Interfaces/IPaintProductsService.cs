using System;
using PaintManagementSystem.API.DTOs;
using PaintManagementSystem.API.Services;

namespace PaintManagementSystem.API.Services.Interfaces;


public interface IPaintProductsService
{
    List<PaintReturnDTO> GetAllPaintProducts();
    PaintReturnDTO? GetPaintProductById(int id);
    PaintReturnDTO? UpdatePaintProduct(int id, PaintDTO paintDTO);
    PaintReturnDTO? CreatePaintProduct(PaintDTO paintDTO);
    PaintReturnDTO? DeletePaintProduct(int id);
}
