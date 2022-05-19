using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

internal class ProductDAO
{
    private static ProductDAO instance = null;
    private static readonly object instanceLock = new object();

    private ProductDAO()
    {
    }

    public static ProductDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }

                return instance;
            }
        }
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        var context = new FStoreDBContext();
        return await context.Products.ToListAsync();
    }

    public async Task<Product?> Get(int id)
    {
        var context = new FStoreDBContext();
        Product? product = await context.Products.Where(product => product.ProductId == id).FirstOrDefaultAsync();
        return product;
    }

    public async Task Add(Product product)
    {
        var context = new FStoreDBContext();
        context.Products.Add(product);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var context = new FStoreDBContext();
        Product product = new Product() { ProductId = id };
        context.Products.Attach(product);
        context.Products.Remove(product);
        await context.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        if ((await Get(product.ProductId)) != null)
        {
            var context = new FStoreDBContext();
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }
    }
}