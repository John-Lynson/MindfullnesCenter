using MFC.CORE.Models;
using MFC.CORE.Interfaces;
using System.Threading.Tasks;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<User> RegisterUserAsync(string email, string name, string password)
    {
        var newUser = new User
        {
            Email = email,
            Name = name,
            RegistrationDate = DateTime.UtcNow,
            // Additional properties like Role could be set here
        };

        await _accountRepository.AddAsync(newUser);
        return newUser;
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _accountRepository.GetByIdAsync(id);
    }

    // More methods for account management
}
