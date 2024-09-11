using RolePermissionDemo.Shared.ApplicationBase.Common;

namespace RolePermissionDemo.Infrastructures.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        PagingResult<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
