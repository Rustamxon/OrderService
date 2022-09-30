using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderService.Data.IRepositories;
using OrderService.Service.Exceptions;
using OrderService.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Service.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IConfiguration configuration;
    public AuthService(
        IUnitOfWork unitOfWork,
        IConfiguration configuration)
    {
        this.unitOfWork = unitOfWork;   
        this.configuration = configuration;
    }
    public async ValueTask<string> GenerateTokenAsync(string login, string password)
    {
        var user = 
            await this.unitOfWork.Users.GetAsync(user =>
                user.Login.Equals(login) && user.Password.Equals(password));

        if (user is null)
            throw new CustomException(404, "Couldn't find user");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
