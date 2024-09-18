using RolePermissionDemo.Applications.UserModules.Dtos.Permission.KeyPermission;

namespace RolePermissionDemo.Applications.UserModules.Abstracts
{
    public interface IKeyPermissionService
    {
        public void Create(CreateKeyPermissionDto input);
        public void Update(UpdateKeyPermissionDto input);
        public void Delete(int id);
        public KeyPermissionDto FindById(int id);
        public List<KeyPermissionDto> FindAll();
        public List<KeyPermissionDto> FindAllByCurrentUserId();

    }
}
