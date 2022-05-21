using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

internal class OrderDetailDAO
{
    private static OrderDetailDAO instance = null;
    private static readonly object instanceLock = new object();

    private OrderDetailDAO()
    {
    }

    public static OrderDetailDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new OrderDetailDAO();
                }

                return instance;
            }
        }
    }

    public async Task<IEnumerable<OrderDetail>> GetAll()
    {
        var context = new FStoreDBContext();
        return await context.OrderDetails.Include(o => o.Order)
            .Include(o => o.Product).ToListAsync();
    }

    public async Task<OrderDetail?> Get(int productId, int orderId)
    {
        var context = new FStoreDBContext();

        OrderDetail? orderDetail = await context.OrderDetails
            .Where(orderDetail => orderDetail.OrderId == orderId && orderDetail.ProductId == productId)
            .Include(o => o.Order)
            .Include(o => o.Product)
            .FirstOrDefaultAsync();
        return orderDetail;
    }

    public async Task Add(OrderDetail orderDetail)
    {
        var context = new FStoreDBContext();
        context.OrderDetails.Add(orderDetail);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int productId, int orderId)
    {
        var context = new FStoreDBContext();
        OrderDetail? orderDetailInMemory =
            new OrderDetail() { OrderId = orderId, ProductId = productId };
        context.OrderDetails.Attach(orderDetailInMemory);
        context.OrderDetails.Remove(orderDetailInMemory);
        await context.SaveChangesAsync();
    }

    public async Task Update(OrderDetail orderDetail)
    {
        var context = new FStoreDBContext();
        context.OrderDetails.Update(orderDetail);
        await context.SaveChangesAsync();
    }
}