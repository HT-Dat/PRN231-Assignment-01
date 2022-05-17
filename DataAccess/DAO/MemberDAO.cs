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
    
}