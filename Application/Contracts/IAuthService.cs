using Application.DTOs;

namespace Application.Contracts;

public interface IAuthService
{
    Task Login(LoginDTO loginInfo);
    Task Logout();
    Task SignUp(SignupDTO signupInfo);
    Task GetActiveUser(int userId);
}
