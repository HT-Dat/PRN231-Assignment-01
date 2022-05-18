using BusinessObject;

namespace DataAccess.Repository;

public interface IOrderDetailRepository
{
    Task<IEnumerable<OrderDetail>> GetAll();
    Task<OrderDetail?> Get(int productId, int orderId);
    Task Delete(int productId, int orderId);
    Task Update(OrderDetail orderDetail);
}