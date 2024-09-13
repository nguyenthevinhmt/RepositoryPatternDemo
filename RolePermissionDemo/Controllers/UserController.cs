using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolePermissionDemo.Applications.UserModules.Abstracts;
using RolePermissionDemo.Applications.UserModules.Implements;
using RolePermissionDemo.Shared.WebAPIBase;

namespace RolePermissionDemo.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(ILogger<UserController> logger, IUserServices userServices) : base(logger)
        {
            _userServices = userServices;
        }

        [HttpPost("add-role-to-user")]
        public ApiResponse AddRoleToUser(int roleId, int userId)
        {
            try
            {
                _userServices.AddRoleToUser(roleId, userId);
                return new();
            }
            catch (Exception ex) { 
                return OkException(ex);
            }
        }


        /// <summary>
        /// Gỡ quyền
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("remove-role-to-user")]
        public ApiResponse RemoveRoleToUser(int roleId, int userId)
        {
            try
            {
                _userServices.RemoveRoleFromUser(roleId, userId);
                return new();
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }
    }
}
