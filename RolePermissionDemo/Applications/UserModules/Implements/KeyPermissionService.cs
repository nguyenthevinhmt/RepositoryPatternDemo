using Microsoft.EntityFrameworkCore;
using RolePermissionDemo.Applications.UserModules.Abstracts;
using RolePermissionDemo.Applications.UserModules.Dtos.Permission.KeyPermission;
using RolePermissionDemo.Domains.Entities;
using RolePermissionDemo.Infrastructures.Persistances;
using RolePermissionDemo.Shared.Consts.Exceptions;
using RolePermissionDemo.Shared.Exceptions;
using System.Text.Json;

namespace RolePermissionDemo.Applications.UserModules.Implements
{
    public class KeyPermissionService : IKeyPermissionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public KeyPermissionService(ApplicationDbContext dbContext, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _logger = logger;
        }
        public void Create(CreateKeyPermissionDto input)
        {
            _logger.LogInformation($"{nameof(Create)}: input = {JsonSerializer.Serialize(input)}");
            if(_dbContext.KeyPermission.Any(k => k.PermissionKey == input.PermissionKey))
            {
                throw new UserFriendlyException(ErrorCode.KeyPermissionHasBeenExist);
            }
            var transaction = _dbContext.Database.BeginTransaction();
            var maxOrderPriority = _dbContext.KeyPermission.Where(k => k.ParentId == input.ParentId).Select(c => c.OrderPriority).Max();
            if(input.OrderPriority > maxOrderPriority + 1)
            {
                throw new UserFriendlyException(ErrorCode.KeyPermissionOrderFailed);
            }
            if (input.OrderPriority.HasValue)
            {
                _dbContext.KeyPermission.Where(k => k.ParentId == input.ParentId && k.OrderPriority >= input.OrderPriority)
                                        .ExecuteUpdate(kp => kp.SetProperty(k => k.OrderPriority, k => k.OrderPriority + 1));
                _dbContext.SaveChanges();
            }

            var newKeyPermission = new KeyPermission()
            {
                PermissionKey = input.PermissionKey,
                PermissionLabel = input.PermissionLabel,
                OrderPriority = input.OrderPriority ?? maxOrderPriority + 1,
                ParentId = input.ParentId,
            };

            _dbContext.KeyPermission.Add(newKeyPermission);
            _dbContext.SaveChanges();

            transaction.Commit();
        }

        public void Delete(int id)
        {
            var keyPermission = _dbContext.KeyPermission.FirstOrDefault(x => x.Id == id)
                                ?? throw new UserFriendlyException(ErrorCode.KeyPermissionNotFound);
            var transaction = _dbContext.Database.BeginTransaction();

            _dbContext.KeyPermission.Where(k => k.ParentId == keyPermission.ParentId && k.OrderPriority > keyPermission.OrderPriority)
                                        .ExecuteUpdate(kp => kp.SetProperty(k => k.OrderPriority, k => k.OrderPriority - 1));
            _dbContext.SaveChanges();

            _dbContext.KeyPermission.Remove(keyPermission);
            _dbContext.SaveChanges();

            transaction.Commit();
        }
        public void Update(UpdateKeyPermissionDto input)
        {
            var keyPermission = _dbContext.KeyPermission.FirstOrDefault(x => x.Id == input.Id)
                              ?? throw new UserFriendlyException(ErrorCode.KeyPermissionNotFound);
            var transaction = _dbContext.Database.BeginTransaction();
            var maxOrderPriority = _dbContext.KeyPermission.Where(k => k.Id == input.ParentId).Select(k => k.OrderPriority).Max();
            if (input.OrderPriority > maxOrderPriority + 1)
            {
                throw new UserFriendlyException(ErrorCode.KeyPermissionOrderFailed);
            }
            if (input.OrderPriority < keyPermission.OrderPriority)
            {
                _dbContext.KeyPermission.Where(k => k.ParentId == input.ParentId && k.OrderPriority >= input.OrderPriority && k.OrderPriority < keyPermission.OrderPriority)
                                        .ExecuteUpdate(kp => kp.SetProperty(k => k.OrderPriority, k => k.OrderPriority + 1));
                _dbContext.SaveChanges();
            }
            else if(input.OrderPriority > keyPermission.OrderPriority)
            {
                _dbContext.KeyPermission.Where(k => k.ParentId == input.ParentId && k.OrderPriority <= input.OrderPriority && k.OrderPriority > keyPermission.OrderPriority)
                                        .ExecuteUpdate(kp => kp.SetProperty(k => k.OrderPriority, k => k.OrderPriority - 1));
                _dbContext.SaveChanges();
            }

            keyPermission.OrderPriority = input.OrderPriority;
            keyPermission.ParentId = input.ParentId;
            keyPermission.PermissionLabel = input.PermissionLabel;
            keyPermission.PermissionKey = input.PermissionKey;
            _dbContext.SaveChanges();
            transaction.Commit();


        }

        public List<KeyPermissionDto> FindAllByCurrentUserId()
        {
            throw new NotImplementedException();
        }

        public KeyPermissionDto FindById(int id)
        {
            var keyPermission = _dbContext.KeyPermission.Select(k => new KeyPermissionDto
            {
                Id = k.Id,
                ParentId = k.ParentId,
                PermissionLabel = k.PermissionLabel,
                //Children = k.Children,
                PermissionKey = k.PermissionKey,
                OrderPriority = k.OrderPriority
            }).FirstOrDefault(k => k.Id == id) ?? throw new UserFriendlyException(ErrorCode.KeyPermissionNotFound);
            return keyPermission;
        }

        public List<KeyPermissionDto> FindAll()
        {
            var rootPermissions = _dbContext.KeyPermission
                            .Include(m => m.Children)
                            .Select(m => new KeyPermissionDto
                            {
                                Id = m.Id,
                                ParentId = m.ParentId,
                                PermissionKey = m.PermissionKey,
                                PermissionLabel = m.PermissionLabel,
                                OrderPriority = m.OrderPriority,
                            })
                            .Where(m => m.ParentId == null)
                            .OrderBy(m => m.OrderPriority)
                            .ThenBy(m => m.Id)
                            .ToList();

            foreach (var permission in rootPermissions)
            {
                LoadChildRecursive(permission);
            }

            return rootPermissions;
        }

        private void LoadChildRecursive(KeyPermissionDto permission)
        {
            var childrenPermissions = _dbContext.KeyPermission
                .Include(k => k.Children)
                .Select(k => new KeyPermissionDto
                {
                    Id = k.Id,
                    ParentId = k.ParentId,
                    ParentKey = permission.PermissionKey,
                    PermissionKey = k.PermissionKey,
                    PermissionLabel = k.PermissionLabel,
                    OrderPriority = k.OrderPriority,
                })
                .Where(k => k.ParentId == permission.Id)
                .OrderBy(k => k.OrderPriority)
                .ThenBy(k => k.Id)
                .ToList();

            permission.Children = childrenPermissions;
            foreach (var childrenPermission in childrenPermissions)
            {
                LoadChildRecursive(childrenPermission);
            }
        }
    }
}
