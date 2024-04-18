using MFC.CORE.Models;
using MFC.CORE.Interfaces.Services;
using MFC.CORE.Interfaces.Repositories;
using System.Threading.Tasks;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<User> AuthenticateUserAsync(string auth0UserId)
    {
        // This method should check if the user exists in the DB and authenticate accordingly.
        // If the user does not exist, it might either create a new user or throw an error, based on your application's logic.
        var user = await _accountRepository.GetByIdAsync(auth0UserId);
        if (user == null)
        {
            // Optionally handle new user registration here or return null/error
            return null;
        }
        return user;
    }

    public async Task<User> RegisterUserAsync(string email, string name, string auth0UserId)
    {
        var newUser = new User
        {
            Email = email,
            Name = name,
            Auth0UserId = auth0UserId,
            RegistrationDate = DateTime.UtcNow
        };
        await _accountRepository.AddAsync(newUser);
        return newUser;
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        return await _accountRepository.GetByIdAsync(userId);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _accountRepository.UpdateAsync(user);
    }

    public Task LogOutAsync()
    {
        // Handle logout logic, if applicable in server context
        return Task.CompletedTask;
    }
}
