using MFC.CORE.Models;
using System.Threading.Tasks;
namespace MFC.CORE.Interfaces.Services
{
    public interface IAccountService
    {
        Task<User> AuthenticateUserAsync(string auth0UserId); 
        Task<User> RegisterUserAsync(string email, string name, string auth0UserId);
        Task<User> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);
        Task LogOutAsync();
    }
}
