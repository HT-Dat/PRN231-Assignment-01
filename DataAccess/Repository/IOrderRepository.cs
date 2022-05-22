using BusinessObject;

namespace DataAccess.Repository;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetMany(int? memberId);
    Task<Order?> Get(int id);
    Task Add(Order order);
    Task Delete(int id);
    Task Update(Order order);
}