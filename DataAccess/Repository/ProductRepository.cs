using BusinessObject;
using DataAccess.DAO;

namespace DataAccess.Repository;

public class ProductRepository : IProductRepository
{
    public Task<IEnumerable<Product>> GetMany(string? queryKeyword)
    {
        return ProductDAO.Instance.GetMany(queryKeyword);
    }

    public Task<Product?> Get(int id)
    {
        return ProductDAO.Instance.Get(id);
    }

    public Task Add(Product product)
    {
        return ProductDAO.Instance.Add(product);
    }

    public Task Delete(int id)
    {
        return ProductDAO.Instance.Delete(id);
    }

    public Task Update(Product product)
    {
        return ProductDAO.Instance.Update(product);
    }
}