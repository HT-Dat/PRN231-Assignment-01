using BusinessObject;

namespace DataAccess.Repository;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAll();
    Task<Member?> Get(int id);
    Task Delete(int id);
    Task Update(Member member);
}