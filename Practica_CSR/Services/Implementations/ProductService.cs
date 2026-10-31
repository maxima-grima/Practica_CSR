using Practica_CSR.Entities;
using Practica_CSR.Models.DTOs.Requests;
using Practica_CSR.Models.DTOs.Responses;
using Practica_CSR.Repositories.Interfaces;
using Practica_CSR.Services.Interfaces;

namespace Practica_CSR.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public List<ProductForReadDto> GetAllProducts()
    {
        return _repository.GetAllProducts()
            .Select(ToReadDto)
            .ToList();
    }

    public ProductForReadDto? GetProductById(int id)
    {
        var product = _repository.GetProductById(id);
        return product == null ? null : ToReadDto(product);
    }

    public ProductForReadDto CreateProduct(ProductForCreateDto dto)
    {
      
        if (NameExists(dto.Name, null))
            throw new InvalidOperationException($"Ya existe un producto con el nombre '{dto.Name}'.");

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        _repository.AddProduct(product);
        return ToReadDto(product);
    }

    public void UpdateProduct(int id, ProductForUpdateDto dto)
    {
        var product = _repository.GetProductById(id);
        if (product == null) return;

       
        if (NameExists(dto.Name, id))
            throw new InvalidOperationException($"Ya existe un producto con el nombre '{dto.Name}'.");

        product.Name = dto.Name;
        product.Price = dto.Price;
        _repository.UpdateProduct(product);
    }

    public void DeleteProduct(int id)
    {
        var product = _repository.GetProductById(id);
        if (product == null) return;

        _repository.DeleteProduct(product);
    }

    private bool NameExists(string name, int? excludeId)
    {
        return _repository.GetAllProducts()
            .Any(p => p.Id != excludeId &&
                      string.Equals(p.Name.Trim(), name.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    private static ProductForReadDto ToReadDto(Product product)
    {
        return new ProductForReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
    public List<ProductForReadDto> SearchProductsByName(string name)
    {
        return _repository.SearchProductsByName(name)
            .Select(ToReadDto)
            .ToList();
    }
    public ProductStatsDto GetStats()
    {
        var products = _repository.GetAllProducts();

        if (!products.Any())
        {
            return new ProductStatsDto
            {
                Total = 0,
                AveragePrice = 0,
                MostExpensiveName = string.Empty
            };
        }

        return new ProductStatsDto
        {
            Total = products.Count,
            AveragePrice = products.Average(p => p.Price),
            MostExpensiveName = products.OrderByDescending(p => p.Price).First().Name
        };
    }
}