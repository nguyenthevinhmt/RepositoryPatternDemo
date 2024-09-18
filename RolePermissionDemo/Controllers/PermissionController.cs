using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolePermissionDemo.Applications.UserModules.Abstracts;
using RolePermissionDemo.Applications.UserModules.Dtos.Permission.KeyPermission;
using RolePermissionDemo.Shared.WebAPIBase;

namespace RolePermissionDemo.Controllers
{
    [Authorize]
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ApiControllerBase
    {
        private readonly IPermissionServices _permissionServices;
        private readonly IKeyPermissionService _keyPermissionModuleService;

        public PermissionController(ILogger<PermissionController> logger, IKeyPermissionService keyPermissionModuleService,  IPermissionServices permissionServices) : base(logger)
        {
            _permissionServices = permissionServices;
            _keyPermissionModuleService = keyPermissionModuleService;
        }

        /// <summary>
        /// Danh sách quyền 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
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

        /// <summary>
        /// Lấy cây phân quyền
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-permission-tree")]
        public ApiResponse GetTree()
        {
            try
            {
                return new(_keyPermissionModuleService.FindAll());
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Chi tiết quyền
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("find-by-id/{id}")]
        public ApiResponse GetById(int id)
        {
            try
            {
                return new(_keyPermissionModuleService.FindById(id));
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Thêm mới quyền
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public ApiResponse Create([FromBody] CreateKeyPermissionDto input)
        {
            try
            {
                _keyPermissionModuleService.Create(input);
                return new();
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public ApiResponse Update([FromBody] UpdateKeyPermissionDto input)
        {
            try
            {
                _keyPermissionModuleService.Update(input);
                return new();
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Xóa quyền
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public ApiResponse Delete (int id)
        {
            try
            {
                _keyPermissionModuleService.Delete(id);
                return new();
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }
    }
}
