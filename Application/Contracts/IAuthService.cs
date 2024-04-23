using Application.DTOs;

namespace Application.Contracts;

public interface IAuthService
{
    Task<string> Login(LoginDTO loginInfo);
    Task Logout();
    Task SignUp(SignupDTO signupInfo);
    Task<UserDTO> GetActiveUser();
}
