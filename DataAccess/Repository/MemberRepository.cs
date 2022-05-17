using BusinessObject;
using DataAccess.DAO;

namespace DataAccess.Repository;

public class MemberRepository : IMemberRepository
{
    public Task<IEnumerable<Member>> GetAll()
    {
        return MemberDAO.Instance.GetAllMember();
    }

    public Task<Member> Get(int id)
    {
        return MemberDAO.Instance.GetMember(id);
    }
}