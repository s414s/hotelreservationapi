using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;

namespace Application.Implementations;

public class AuthService : IAuthService
{
    private readonly IUsersRepository _usersRepo;

    public AuthService(IUsersRepository usersRepo)
    {
        _usersRepo = usersRepo;
    }

    public Task<UserDTO> GetActiveUser()
    {
        // TODO - leer info del JWT
        throw new NotImplementedException();
    }

    public async Task Login(LoginDTO loginInfo)
    {
        var user = await _usersRepo.GetByCredentials(loginInfo.Username, loginInfo.Password)
            ?? throw new ApplicationException("username or password are not correct");

        // TODO - crear y devolver el Bearer JWT
    }

    public Task Logout()
    {
        // TODO - borrar info del Bearer
        throw new NotImplementedException();
    }

    public async Task SignUp(SignupDTO signupInfo)
    {
        var newUser = new User(signupInfo.Name, signupInfo.Surname, signupInfo.Password);
        await _usersRepo.Add(newUser);
        await _usersRepo.SaveChanges();
    }
}
