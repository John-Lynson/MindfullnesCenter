using MFC.CORE.Models;
using MFC.CORE.Interfaces.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MFC.DAL.Database;

namespace MFC.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MFCContext _context;

        public AccountRepository(MFCContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            return await _context.Users.FirstOrDefaultAsync(u => u.Auth0UserId == userId);
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
