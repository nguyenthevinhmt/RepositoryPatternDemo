using RolePermissionDemo.Applications.UserModules.Dtos.Permission.KeyPermission;
using RolePermissionDemo.Shared.ApplicationBase.Common.Validations;

namespace RolePermissionDemo.Applications.UserModules.Dtos.ConfigPermission
{
    public class CreatePermissionApiDto
    {
        [CustomMaxLength(500)]
        public string Path { get; set; } = null!;
        [CustomMaxLength(500)]
        public string? Description { get; set; }
        public List<CreateKeyPermissionDto> PermissionKeys { get; set; } = null!;

    }
}
