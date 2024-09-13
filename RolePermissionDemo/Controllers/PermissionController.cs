using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolePermissionDemo.Applications.UserModules.Abstracts;
using RolePermissionDemo.Shared.WebAPIBase;

namespace RolePermissionDemo.Controllers
{
    [Authorize]
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ApiControllerBase
    {
        private readonly IPermissionServices _permissionServices;

        public PermissionController(ILogger<PermissionController> logger, IPermissionServices permissionServices) : base(logger)
        {
            _permissionServices = permissionServices;
        }

        /// <summary>
        /// Danh sách quyền 
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-all")]
        public ApiResponse FindAll()
        {
            try
            {
                return new(_permissionServices.FindAll());
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Lấy tất cả quyền của user hiện tại
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-permissions-by-user")]
        public ApiResponse GetPermissionsByCurrentUserId()
        {
            try
            {
                return new(_permissionServices.GetPermissionsByCurrentUserId());
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Check quyền
        /// </summary>
        /// <returns></returns>
        [HttpPost("check-permission")]
        public ApiResponse CheckPermission([FromBody] string[] permissionKeys)
        {
            try
            {
                return new(_permissionServices.CheckPermission(permissionKeys));
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

    }
}
