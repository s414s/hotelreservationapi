using Application.Contracts;
using Application.DTOs;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enum;
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

    public AuthService(IUsersRepository usersRepo, IConfiguration config)
    {
        _usersRepo = usersRepo;
        _jwtKey = config["JWT:Key"] ?? throw new ArgumentNullException("missing jwt key");
        _jwtIssuer = config["JWT:Issuer"] ?? throw new ArgumentNullException("missing jwt issuer");
        _jwtAudience = config["JWT:Audience"] ?? throw new ArgumentNullException("missing jwt audience");
    }

    public Task<UserDTO> GetActiveUser()
    {
        // TODO - leer info del JWT
        throw new NotImplementedException();
    }

    public async Task<string> Login(LoginDTO loginInfo)
    {
        var user = await _usersRepo.GetByCredentials(loginInfo.Username, loginInfo.Password)
            ?? throw new ApplicationException("username or password are not correct");

        //return GenerateJWT(user.Id, user.Role);
        return GenerateJWTTokens(user.Id, user.Role);

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

    private string GenerateJWT(long userId, Roles role)
    {
        // Define security key and token parameters
        // OJO que en el otro lo hace distinto
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

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

        // Create token
        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(1), // Token expiration time
            signingCredentials: credentials
        );

        var payload = token.Payload;

        // Serialize token to string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // https://codepedia.info/aspnet-core-jwt-refresh-token-authentication
    public string GenerateJWTTokens(long userId, Roles role)
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
                    new("role", role.ToString())
                }),
            Expires = DateTime.Now.AddMinutes(5),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        // ===============
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

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

        var payloaddd = testOfJWT.Payload;
        var tokenazo = tokenHandler.WriteToken(testOfJWT);

        // ===============
        return tokenHandler.WriteToken(token);
    }

}
