using RolePermissionDemo.Applications.UserModules.Dtos.Permission.KeyPermission;
using RolePermissionDemo.Shared.ApplicationBase.Common.Validations;

namespace RolePermissionDemo.Applications.UserModules.Dtos.ConfigPermission
{
    public class UpdatePermissionConfigDto
    {
        public int Id { get; set; }
        [CustomMaxLength(500)]
        public string Path { get; set; } = null!;
        [CustomMaxLength(500)]
        public string? Description {  get; set; }
        public List<UpdateKeyPermissionDto> PermissionKeys { get; set; } = null!;
    }
}
