using MFC.CORE.Interfaces.Services;
using MFC.CORE.Models;
using MFC.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MFC.DAL.Services
{
    public class UserService : IUserService
    {
        private readonly MFCContext _context;

        public UserService(MFCContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            var existingUser = await _context.Users.FindAsync(newUser.Id);
            if (existingUser == null)
            {
                newUser.RegistrationDate = DateTime.UtcNow;
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
            }

            return newUser;
        }

        public async Task<User> UpdateUserAsync(User updatedUser)
        {
            var user = await _context.Users.FindAsync(updatedUser.Id);
            if (user != null)
            {
                user.Email = updatedUser.Email;
                user.Name = updatedUser.Name;
                user.Role = updatedUser.Role;
                user.LastLoginDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
