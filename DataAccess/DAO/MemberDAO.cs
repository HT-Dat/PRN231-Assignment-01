using System.Reflection;
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

    public async Task<Member> Authentication(string email, string password)
    {
        var context = new FStoreDBContext();
        var list = context.Members.ToList();
        return list.Where(member =>
                member.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && member.Password.Equals(password))
            .FirstOrDefault();
    }
    public async Task<IEnumerable<Member>> GetAll()
    {
        var context = new FStoreDBContext();
        return await context.Members.ToListAsync();
    }

    public async Task<Member?> Get(int id)
    {
        var context = new FStoreDBContext();
        Member? member = await context.Members.Where(member => member.MemberId == id).FirstOrDefaultAsync();
        return member;
    }

    public async Task Add(Member member)
    {
        var context = new FStoreDBContext();
        context.Members.Add(member);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        if ((await Get(id)) != null)
        {
            var context = new FStoreDBContext();
            Member member = new Member() { MemberId = id };
            context.Members.Attach(member);
            context.Members.Remove(member);
            await context.SaveChangesAsync();
        }
    }

    public async Task Update(Member member)
    {
        var context = new FStoreDBContext();
        context.Members.Update(member);
        await context.SaveChangesAsync();
    }
}