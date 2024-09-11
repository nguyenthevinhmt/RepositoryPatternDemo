using RolePermissionDemo.Applications.UserModules.Dtos;

namespace RolePermissionDemo.Applications.UserModules.Abstracts
{
    public interface IAuthService
    {
        public TokenApiDto Login(UserLoginDto user);
        //public TokenApiDto RefreshToken(TokenApiDto input);
        public void RegisterUser(CreateUserDto user);
    }
}
