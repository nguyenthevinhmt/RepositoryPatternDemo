﻿using RolePermissionDemo.Applications.UserModules.Dtos.Permission;

namespace RolePermissionDemo.Applications.UserModules.Abstracts
{
    public interface IPermissionServices
    {
        /// <summary>
        /// Danh sách quyền
        /// </summary>
        /// <returns></returns>
        List<PermissionDto> FindAll();
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
    }
}
