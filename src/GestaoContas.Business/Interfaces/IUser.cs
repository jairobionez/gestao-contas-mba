using System.Security.Claims;

namespace GestaoContas.Business.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetId();
        string GetEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
