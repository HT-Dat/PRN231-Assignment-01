using BusinessObject;
using DataAccess.DAO;

namespace DataAccess.Repository;

public class MemberRepository : IMemberRepository
{
    public Task<IEnumerable<Member>> GetAll()
    {
        return MemberDAO.Instance.GetAll();
    }

    public Task<Member?> Get(int id)
    {
        return MemberDAO.Instance.Get(id);
    }

    public Task Delete(int id)
    {
        return MemberDAO.Instance.Delete(id);
    }

    public Task Update(Member member)
    {
        return MemberDAO.Instance.Update(member);

    }
}