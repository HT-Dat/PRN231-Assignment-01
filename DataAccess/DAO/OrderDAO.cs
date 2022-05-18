using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

internal class OrderDAO
{
    private static OrderDAO instance = null;
    private static readonly object instanceLock = new object();

    private OrderDAO()
    {
    }

    public static OrderDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new OrderDAO();
                }

                return instance;
            }
        }
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        var context = new FStoreDBContext();
        return await context.Orders.ToListAsync();
    }

    public async Task<Order?> Get(int id)
    {
        var context = new FStoreDBContext();
        Order? order = await context.Orders.Where(order => order.OrderId == id).FirstOrDefaultAsync();
        return order;
    }

    public async Task Delete(int id)
    {
        if ((await Get(id)) != null)
        {
            var context = new FStoreDBContext();
            Order order = new Order() { OrderId = id };
            context.Orders.Attach(order);
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }
    }

    public async Task Update(Order order)
    {
        if ((await Get(order.OrderId)) != null)
        {
            var context = new FStoreDBContext();
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
    }
}