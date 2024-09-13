using RolePermissionDemo.Domains.Entities;

namespace RolePermissionDemo.Applications.UserModules.Dtos.Role
{
    public class DetailRoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<string>? PermissionKeys { get; set; }
    }
}
