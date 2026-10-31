using Practica_CSR.Models.DTOs.Requests;
using Practica_CSR.Models.DTOs.Responses;

namespace Practica_CSR.Services.Interfaces;

public interface IProductService
{
    List<ProductForReadDto> GetAllProducts();
    ProductForReadDto? GetProductById(int id);
    ProductForReadDto CreateProduct(ProductForCreateDto dto);
    void UpdateProduct(int id, ProductForUpdateDto dto);
    void DeleteProduct(int id);
    List<ProductForReadDto> SearchProductsByName(string name);
    ProductStatsDto GetStats();
}