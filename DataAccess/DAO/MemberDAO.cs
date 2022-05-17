using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

internal class MemberDAO
{
    private static MemberDAO instance = null;
    private static readonly object instanceLock = new object();

    private MemberDAO()
    {
    }

    public static MemberDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new MemberDAO();
                }

                return instance;
            }
        }
    }

    public async Task<IEnumerable<Member>> GetAllMember()
    {
        var context = new FStoreDBContext();
        return await context.Members.ToListAsync();
    }

    public async Task<Member?> GetMember(int? id)
    {
        var context = new FStoreDBContext();
        Member? member = await context.Members.Where(member => member.MemberId == id).FirstOrDefaultAsync();

        if (member == null)
        {
            throw new NullReferenceException();
        }
        return member;
    }
}