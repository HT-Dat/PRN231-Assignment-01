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

    public async Task<IEnumerable<Order>> GetMany(int? memberId)
    {
        var context = new FStoreDBContext();
        if (memberId == null)
        {
            return await context.Orders.Include(x => x.Member).ToListAsync();
        }
        return await context.Orders.Where(x => x.MemberId == memberId).Include(x => x.Member).ToListAsync();
    }

    public async Task<Order?> Get(int id)
    {
        var context = new FStoreDBContext();
        Order? order = await context.Orders.Where(order => order.OrderId == id).Include(x => x.Member)
            .FirstOrDefaultAsync();
        return order;
    }

    public async Task Add(Order order)
    {
        var context = new FStoreDBContext();
        context.Orders.Add(order);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var context = new FStoreDBContext();
        Order order = new Order() { OrderId = id };
        context.Orders.Attach(order);
        context.Orders.Remove(order);
        await context.SaveChangesAsync();
    }

    public async Task Update(Order order)
    {
        var context = new FStoreDBContext();
        context.Orders.Update(order);
        await context.SaveChangesAsync();
    }
}