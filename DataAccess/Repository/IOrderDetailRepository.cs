using BusinessObject;

namespace DataAccess.Repository;

public interface IOrderDetailRepository
{
    Task<IEnumerable<OrderDetail>> GetAll();
    Task<IEnumerable<OrderDetail>> Get(int? productId, int orderId);
    Task Add(OrderDetail orderDetail);
    Task Delete(int productId, int orderId);
    Task Update(OrderDetail orderDetail);
}