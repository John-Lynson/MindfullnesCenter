using System.Threading.Tasks;
using MFC.CORE.Models;

namespace MFC.CORE.Interfaces.Services
{
    public interface IAccountService
    {
        Task<User> AuthenticateUserAsync(string email, string password);
        Task<User> RegisterUserAsync(string email, string password, string name);
        Task<User> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);
        Task LogOutAsync();
    }
}
