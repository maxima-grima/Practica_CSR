using Practica_CSR.Data;
using Practica_CSR.Entities;
using Practica_CSR.Repositories.Interfaces;

namespace Practica_CSR.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly Practica_CSRContext _context;

    public ProductRepository(Practica_CSRContext context)
    {
        _context = context;
    }

    public List<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public Product? GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        var existing = _context.Products.FirstOrDefault(p => p.Id == product.Id);
        if (existing == null) return;

        existing.Name = product.Name;
        existing.Price = product.Price;
        _context.SaveChanges();
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public List<Product> SearchProductsByName(string name)
    {
        var text = name.ToLower();
        return _context.Products
            .Where(p => p.Name.ToLower().Contains(text))
            .ToList();
    }
}
