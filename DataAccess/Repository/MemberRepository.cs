using BusinessObject;
using DataAccess.DAO;

namespace DataAccess.Repository;

public class MemberRepository : IMemberRepository 
{
    public Task<Member> Authentication(string email, string password)
    {
        return MemberDAO.Instance.Authentication(email, password);
    }

    public Task<IEnumerable<Member>> GetAll()
    {
        return MemberDAO.Instance.GetAll();
    }

    public Task<Member?> Get(int id)
    {
        return MemberDAO.Instance.Get(id);
    }

    public Task Add(Member member)
    {
        return MemberDAO.Instance.Add(member);
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