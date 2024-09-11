using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolePermissionDemo.Applications.UserModules.Abstracts;
using RolePermissionDemo.Applications.UserModules.Dtos;
using RolePermissionDemo.Shared.WebAPIBase;

namespace RolePermissionDemo.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService) : base(logger)
        {
            _authService = authService;
        }

        /// <summary>
        /// Đăng nhập 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public ApiResponse Login([FromBody] UserLoginDto input)
        {
            try
            {
                return new(_authService.Login(input));
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }
        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public ApiResponse Register([FromBody] CreateUserDto input)
        {
            try
            {
                _authService.RegisterUser(input);
                return new();
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

    }
}
