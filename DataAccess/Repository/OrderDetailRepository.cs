using BusinessObject;
using DataAccess.DAO;

namespace DataAccess.Repository;

public class OrderDetailRepository : IOrderDetailRepository
{
    public Task<IEnumerable<OrderDetail>> GetAll()
    {
        return OrderDetailDAO.Instance.GetAll();
    }

    public Task<IEnumerable<OrderDetail>> Get(int? productId, int orderId)
    {
        return OrderDetailDAO.Instance.Get(productId, orderId);
    }

    public Task Add(OrderDetail orderDetail)
    {
        return OrderDetailDAO.Instance.Add(orderDetail);
    }

    public Task Delete(int productId, int orderId)
    {
        return OrderDetailDAO.Instance.Delete(productId, orderId);
    }

    public Task Update(OrderDetail orderDetail)
    {
        return OrderDetailDAO.Instance.Update(orderDetail);
    }
}