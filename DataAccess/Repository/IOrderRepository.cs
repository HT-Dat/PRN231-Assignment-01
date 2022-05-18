using BusinessObject;

namespace DataAccess.Repository;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAll();
    Task<Order?> Get(int id);
    Task Delete(int id);
    Task Update(Order order);
}