using BusinessObject;

namespace DataAccess.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetMany(string? queryKeyword);
    Task<Product?> Get(int id);
    Task Add(Product product);
    Task Delete(int id);
    Task Update(Product product);
}