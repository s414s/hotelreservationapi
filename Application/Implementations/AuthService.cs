using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Application.Implementations;

public class AuthService : IAuthService
{
    private readonly IUsersRepository _usersRepo;
    //private readonly IConfiguration _config;
    private readonly string _jwtKey;
    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IUsersRepository usersRepo, IConfiguration config, IHttpContextAccessor httpCA)
    {
        _usersRepo = usersRepo;
        _httpContextAccessor = httpCA;
        _jwtKey = config["JWT:Key"] ?? throw new ArgumentNullException("missing jwt key");
        _jwtIssuer = config["JWT:Issuer"] ?? throw new ArgumentNullException("missing jwt issuer");
        _jwtAudience = config["JWT:Audience"] ?? throw new ArgumentNullException("missing jwt audience");
    }

    public async Task<UserDTO> GetActiveUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst("id")?.Value
                ?? throw new ApplicationException("user not found");

            var role = claimsIdentity?.FindFirst("userrole")?.Value
                ?? throw new ApplicationException("no role found");

            var userEntity = await _usersRepo.GetByID(long.Parse(userId))
                ?? throw new ApplicationException("user not found");

            return UserDTO.MapFromDomainEntity(userEntity);
        }
        else
        {
            throw new ApplicationException("you are not authenticated");
        }
    }

    public async Task<string> Login(LoginDTO loginInfo)
    {
        var user = await _usersRepo.GetByCredentials(loginInfo.Username, loginInfo.Password)
            ?? throw new ApplicationException("username or password are not correct");

        //return GenerateJWT(user.Id, user.Role);
        return GenerateJWT(user.Id, user.Role);

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

    // https://codepedia.info/aspnet-core-jwt-refresh-token-authentication
    public string GenerateJWT(long userId, Roles role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtIssuer,
            Audience = _jwtAudience,
            Subject = new ClaimsIdentity(new Claim[]
                {
                    new("id", userId.ToString()),
                    new("userrole", role.ToString())
                }),
            Expires = DateTime.Now.AddMinutes(5),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        // ===============
        var securityKey = new SymmetricSecurityKey(tokenKey);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        // TODO - private claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), // user id
            new Claim(JwtRegisteredClaimNames.Iss, _jwtIssuer), // issuer
            new Claim(JwtRegisteredClaimNames.Aud, _jwtAudience), // audience
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()), // issued at time
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // jwt id
            new Claim("role", role.ToString()),
        };

        var testOfJWT = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(1), // Token expiration time
            signingCredentials: credentials);

        // TODO delete this
        //var payloaddd = testOfJWT.Payload;
        //var tokenazo = tokenHandler.WriteToken(testOfJWT);
        // ===============

        return tokenHandler.WriteToken(token);
        //return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
