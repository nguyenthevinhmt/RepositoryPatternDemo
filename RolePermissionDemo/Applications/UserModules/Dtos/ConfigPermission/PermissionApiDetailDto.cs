﻿using RolePermissionDemo.Applications.UserModules.Dtos.Permission.KeyPermission;

namespace RolePermissionDemo.Applications.UserModules.Dtos.ConfigPermission
{
    public class PermissionApiDetailDto
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public string? Description { get; set; }
        public List<KeyPermissionDto>? KeyPermissions {  get; set; }
        public object? Parent {  get; set; }
    }
}
