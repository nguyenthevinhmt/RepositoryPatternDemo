using Microsoft.EntityFrameworkCore;
using RolePermissionDemo.Applications.UserModules.Abstracts;
using RolePermissionDemo.Applications.UserModules.Dtos.Permission;
using RolePermissionDemo.Domains.Entities;
using RolePermissionDemo.Infrastructures.Persistances;
using RolePermissionDemo.Shared.ApplicationBase.Common;
using RolePermissionDemo.Shared.Consts;
using RolePermissionDemo.Shared.Consts.Permissions;

namespace RolePermissionDemo.Applications.UserModules.Implements
{
    public class PermissionServices : IPermissionServices
    {
        private readonly ILogger<PermissionServices> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public PermissionServices(ILogger<PermissionServices> logger, ApplicationDbContext dbContext, IHttpContextAccessor httpContext = null)
        {
            _logger = logger;
            _dbContext = dbContext;
            _httpContext = httpContext;
        }
        public bool CheckPermission(string[] permissionKeys)
        {
            var currentUserId = _httpContext.GetCurrentUserId();
            var currentUserType = _httpContext.GetCurrentUserType();
            _logger.LogInformation($"{nameof(CheckPermission)}: permissionKeys = {permissionKeys}, userId: {currentUserId}, userType: {currentUserType}");
            return currentUserType == UserTypes.ADMIN || GetListPermissionKeyContains(currentUserId, permissionKeys).Any(); ;
        }

        public List<PermissionDto> FindAll()
        {
            _logger.LogInformation($"{nameof(FindAll)}");
            var result = PermisisonConfig.appConfigs.Select(p => new PermissionDto
            {
                PermisisonKey = p.Key,
                PermissionLabel = p.Value.PermissionLabel,
                ParentKey = p.Value.ParentKey ?? ""
            }).ToList();
            return result;
        }

        public List<string> GetPermissionsByCurrentUserId()
        {
            var currentUserId = _httpContext.GetCurrentUserId();
            var currentUserType = _httpContext.GetCurrentUserType();
            _logger.LogInformation($"{nameof(GetPermissionsByCurrentUserId)}: userId: {currentUserId}, userType: {currentUserType}");

            var result = new List<string>();
            if(currentUserType == UserTypes.ADMIN)
            {
                var temp = PermisisonConfig.appConfigs.Select(c => c.Key);
                result.AddRange(PermisisonConfig.appConfigs.Select(c => c.Key));
            }
            else
            {
                result = (from user in _dbContext.Users
                          join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                          join role in _dbContext.Roles on userRole.RoleId equals role.Id
                          join rolePermission in _dbContext.RolePermissions on role.Id equals rolePermission.RoleId
                          into rps
                          from rp in rps
                          where user.Id == currentUserId && !user.Deleted && role.Status == CommonStatus.ACTIVE && !userRole.Deleted
                          select rp.PermissionKey).ToList();
            }
            return result;
        }

        private IQueryable<string?> GetListPermissionKeyContains(
            int userId,
            string[] permissionKeys
        )
        {
            return from userRole in _dbContext.UserRoles
                   join role in _dbContext.Roles on userRole.RoleId equals role.Id
                   join rolePermission in _dbContext.RolePermissions
                       on role.Id equals rolePermission.RoleId
                   where
                       userRole.UserId == userId
                       && !role.Deleted
                       && !userRole.Deleted
                       && role.Status == CommonStatus.ACTIVE
                       && permissionKeys.Contains(rolePermission.PermissionKey)
                   select rolePermission.PermissionKey;
        }
    }
}
