using MFC.CORE.Models;
using System.Threading.Tasks;

namespace MFC.CORE.Interfaces
{
    public interface IAccountRepository
    {
        Task AddAsync(User user);
        Task<User> GetByIdAsync(string userId);
        Task UpdateAsync(User user);
    }
}
