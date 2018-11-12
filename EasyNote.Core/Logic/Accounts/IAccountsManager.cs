using EasyNote.Core.Model.Accounts;
using System.Threading.Tasks;

namespace EasyNote.Core.Logic.Accounts
{
    public interface IAccountsManager
    {
        Task CreateUserAsync(CredentialsParams credentialsParams);
        Task<string> GetUserTokenAsync(CredentialsParams credentials);
    }
}
