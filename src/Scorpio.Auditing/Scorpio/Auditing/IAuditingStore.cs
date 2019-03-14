using System.Threading.Tasks;

namespace Scorpio.Auditing
{
    public interface IAuditingStore
    {
        Task SaveAsync(AuditInfo info);
    }
}