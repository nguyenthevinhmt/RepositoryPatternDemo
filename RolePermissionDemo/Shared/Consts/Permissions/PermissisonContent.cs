﻿namespace RolePermissionDemo.Shared.Consts.Permissions
{
    public class PermissisonContent
    {
        /// <summary>
        /// Key cha
        /// </summary>
        public string? ParentKey { get; set; }
        /// <summary>
        /// Key permission hiện tại
        /// </summary>
        public string PermissionKey { get; set; }
        public string PermissionLabel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionKey"></param>
        /// <param name="parentKey"></param>
        public PermissisonContent(string permissionKey, string permissionLabel, string? parentKey = null)
        {
            PermissionKey = permissionKey;
            PermissionLabel = permissionLabel;
            ParentKey = parentKey;
        }
    }
}
