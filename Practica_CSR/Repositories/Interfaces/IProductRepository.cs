using Practica_CSR.Entities;

namespace Practica_CSR.Repositories.Interfaces;

public interface IProductRepository
{
    List<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    List<Product> SearchProductsByName(string name);
}