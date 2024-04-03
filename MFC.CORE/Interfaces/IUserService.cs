using MFC.CORE.Models;
using System.Threading.Tasks;

namespace MFC.CORE.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User newUser);
        Task<User> UpdateUserAsync(User updatedUser);
        Task<User> GetUserByIdAsync(string userId);
    }
}
