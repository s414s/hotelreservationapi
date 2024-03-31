using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;

namespace Application.Implementations;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _usersRepo;

    public AuthService(IRepository<User> usersRepo)
    {
        _usersRepo = usersRepo;
    }

    public Task<UserDTO> GetActiveUser()
    {
        throw new NotImplementedException();
    }

    public Task Login(LoginDTO loginInfo)
    {
        throw new NotImplementedException();
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }

    public Task SignUp(SignupDTO signupInfo)
    {
        throw new NotImplementedException();
    }
}
