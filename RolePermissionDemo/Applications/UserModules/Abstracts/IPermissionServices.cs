using RolePermissionDemo.Applications.UserModules.Dtos.Permission;
using RolePermissionDemo.Applications.UserModules.Dtos.Permission.KeyPermission;
using RolePermissionDemo.Shared.ApplicationBase.Common;

namespace RolePermissionDemo.Applications.UserModules.Abstracts
{
    public interface IPermissionServices
    {
        /// <summary>
        /// Check permission
        /// </summary>
        /// <param name="permissionKeys"></param>
        /// <returns></returns>
        bool CheckPermission(params string[] permissionKeys);

        /// <summary>
        /// Lấy tất cả quyền của user hiện tại
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<string> GetPermissionsByCurrentUserId();

        /// <summary>
        /// Danh sách quyền fixed
        /// </summary>
        /// <returns></returns>
        List<PermissionDto> FindAll();

        /// <summary>
        /// Lấy all permission dựa vào api path
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        string[] GetAllPermissionKeyByApiEndpoint(string api); 



    }
}
