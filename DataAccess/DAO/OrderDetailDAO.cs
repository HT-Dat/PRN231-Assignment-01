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
        return await context.OrderDetails.ToListAsync();
    }

    public async Task<OrderDetail?> Get(int productId, int orderId)
    {
        var context = new FStoreDBContext();
        try
        {
            Console.WriteLine("Ibrrr");

            OrderDetail? orderDetail = await context.OrderDetails
                .Where(orderDetail => orderDetail.OrderId == orderId && orderDetail.ProductId == productId)
                .FirstOrDefaultAsync();
            Console.WriteLine("I crashed here");

            return orderDetail;

        }
        catch (Exception)
        {

        }
        return null;

        
    }

    public async Task Delete(int productId, int orderId)
    {
        OrderDetail? orderDetailInMemory;
        Console.WriteLine("ACBCBC");
        if ((orderDetailInMemory = (await Get(productId, orderId))) != null)
        {
            
            var context = new FStoreDBContext();
            context.OrderDetails.Remove(orderDetailInMemory);
            await context.SaveChangesAsync();
        }

    }

    public async Task Update(OrderDetail orderDetail)
    {
        OrderDetail? orderDetailInMemory;

        if ((orderDetailInMemory = await Get(orderDetail.ProductId,orderDetail.OrderId)) != null)
        {
            var context = new FStoreDBContext();
            context.OrderDetails.Update(orderDetailInMemory);
            await context.SaveChangesAsync();
        }
    }
}