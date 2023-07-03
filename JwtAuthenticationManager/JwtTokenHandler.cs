using Authentication.Services;
using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "topsecretsecretJWTsigningKey@12356789";
        public const int JWT_TOKEN_VALIDITY_MINS = 20;
        UserService _userService = new UserService();
        public JwtTokenHandler()
        {
             
        }
        public async Task<string?> GenerateAuthToken(LoginModel loginModel)
        {
            Users? user = await _userService.AuthenticateAsync(loginModel.UserName, loginModel.Password);

            if (user is null)
            {
                return null;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("topsecretsecretJWTsigningKey@12356789"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expirationTimeStamp = DateTime.Now.AddMinutes(50);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, user.UserID),
            new Claim("Role", user.Role)
            //new Claim("scope", string.Join(" ", user.Scopes))
        };

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5002",
                claims: claims,
                expires: expirationTimeStamp,
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

    }
}
