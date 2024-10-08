﻿using Microsoft.IdentityModel.Tokens;
using RolePermissionDemo.Applications.UserModules.Abstracts;
using RolePermissionDemo.Applications.UserModules.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using RolePermissionDemo.Infrastructures.Persistances;
using RolePermissionDemo.Shared.Exceptions;
using RolePermissionDemo.Shared.Consts.Exceptions;
using RolePermissionDemo.Shared.Utils;
using RolePermissionDemo.Domains.Entities;
using RolePermissionDemo.Shared.Consts;
using System.Text.Json;

namespace RolePermissionDemo.Applications.UserModules.Implements
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        public TokenApiDto Login(UserLoginDto userInput)
        {
            _logger.LogInformation($"{nameof(Login)}: input = {JsonSerializer.Serialize(userInput)}");
            var user = _context.Users.FirstOrDefault(x => x.Username == userInput.Username);
            if (user == null)
            {
                throw new UserFriendlyException(ErrorCode.UserNotFound);
            }
            if (!PasswordHasher.VerifyPassword(userInput.Password, user.Password))
            {
                throw new UserFriendlyException(ErrorCode.PasswordWrong);
            }
            var newAccessToken = CreateJwt(userInput);
            //var newRefreshToken = CreateRefreshToken();
            //user.RefreshToken = newRefreshToken;
            //user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1);
            _context.SaveChangesAsync();
            return new TokenApiDto
            {
                AccessToken = newAccessToken,
                //RefreshToken = newRefreshToken
            };
        }
        public void RegisterUser(CreateUserDto user)
        {
            _logger.LogInformation($"{nameof(RegisterUser)}: input = {JsonSerializer.Serialize(user)}");
            var check = _context.Users.FirstOrDefault(x => x.Username == user.Username);
            if (check != null)
            {
                throw new UserFriendlyException(ErrorCode.UsernameIsExist);
            }
            if (user.Password.Length < 6)
            {
                throw new UserFriendlyException(ErrorCode.PasswordMustBeLongerThanSixCharacter);
            }
            if (!(Regex.IsMatch(user.Password, "[a-z]") && Regex.IsMatch(user.Password, "[A-Z]") && Regex.IsMatch(user.Password, "[0-9]")))
            {
                throw new UserFriendlyException(ErrorCode.TypeofPasswordMustBeNumberOrString);
            }
            if (!Regex.IsMatch(user.Password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                throw new UserFriendlyException(ErrorCode.PasswordMustBeContainsSpecifyCharacter);

            _context.Users.Add(new User
            {
                Username = user.Username,
                Password = PasswordHasher.HashPassword(user.Password),
                UserType = user.UserType,
            });
            _context.SaveChanges();
        }
        //public TokenApiDto RefreshToken(TokenApiDto input)
        //{
        //    if (input is null)
        //        throw new UserFriendlyException("Invalid Client Request");
        //    string accessToken = input.AccessToken;
        //    string refreshToken = input.RefreshToken;
        //    var principal = GetPrincipleFromExpiredToken(accessToken);
        //    var email = principal.Identity.Name;
        //    var user = _context.Users.FirstOrDefault(u => u.Email == email);
        //    if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        //        throw new UserFriendlyException("Invalid Request");
        //    var newAccessToken = CreateJwt(new UserLoginDto { Email = user.Email, Password = user.Password });
        //    var newRefreshToken = CreateRefreshToken();
        //    user.RefreshToken = newRefreshToken;
        //    _context.SaveChangesAsync();
        //    return new TokenApiDto()
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken,
        //    };
        //}
        private string CreateJwt(UserLoginDto user)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var userId = _context.Users.FirstOrDefault(u => u.Username == user.Username) ?? throw new UserFriendlyException(ErrorCode.UserNotFound);

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT")["Key"]!);
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, $"{userId.Id}"),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("user_type", userId.UserType.ToString()),
                new Claim("user_id", userId.Id.ToString())
            };
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: credentials
            );
            return jwtToken.WriteToken(token);
        }
        //private string CreateRefreshToken()
        //{
        //    var tokenBytes = RandomNumberGenerator.GetBytes(64);
        //    var refreshToken = Convert.ToBase64String(tokenBytes);
        //    var tokenInUser = _context.Users.Any(a => a.RefreshToken == refreshToken);
        //    if (tokenInUser)
        //    {
        //        return CreateRefreshToken();
        //    }
        //    return refreshToken;
        //}
        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("JWT")["Key"]!)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new UserFriendlyException(ErrorCode.LoginExpired);
            return principal;
        }
    }
}
