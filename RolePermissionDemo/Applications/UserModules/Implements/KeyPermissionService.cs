using Microsoft.EntityFrameworkCore;
using RolePermissionDemo.Applications.UserModules.Abstracts;
using RolePermissionDemo.Applications.UserModules.Dtos.ConfigPermission;
using RolePermissionDemo.Applications.UserModules.Dtos.Permission.KeyPermission;
using RolePermissionDemo.Domains.Entities;
using RolePermissionDemo.Infrastructures.Persistances;
using RolePermissionDemo.Shared.ApplicationBase.Common;
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
            if (_dbContext.KeyPermission.Any(k => k.PermissionKey == input.PermissionKey))
            {
                throw new UserFriendlyException(ErrorCode.KeyPermissionHasBeenExist);
            }
            var transaction = _dbContext.Database.BeginTransaction();
            var maxOrderPriority = _dbContext.KeyPermission.Where(k => k.ParentId == input.ParentId).Select(c => c.OrderPriority).Max();
            if (input.OrderPriority > maxOrderPriority + 1)
            {
                throw new UserFriendlyException(ErrorCode.KeyPermissionOrderFailed);
            }
            _dbContext.KeyPermission.Where(k => k.ParentId == input.ParentId && k.OrderPriority >= input.OrderPriority)
                                    .ExecuteUpdate(kp => kp.SetProperty(k => k.OrderPriority, k => k.OrderPriority + 1));
            _dbContext.SaveChanges();

            var newKeyPermission = new KeyPermission()
            {
                PermissionKey = input.PermissionKey,
                PermissionLabel = input.PermissionLabel,
                OrderPriority = input.OrderPriority,
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
            else if (input.OrderPriority > keyPermission.OrderPriority)
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
                            .Where(m => !m.Deleted && m.ParentId == null)
                            .Select(m => new KeyPermissionDto
                            {
                                Id = m.Id,
                                ParentId = m.ParentId,
                                PermissionKey = m.PermissionKey,
                                PermissionLabel = m.PermissionLabel,
                                OrderPriority = m.OrderPriority,
                            })
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

        public void CreatePermissionConfig(CreatePermissionApiDto input)
        {
            _logger.LogInformation($"{nameof(CreatePermissionConfig)}: input = {JsonSerializer.Serialize(input)}");
            var transaction = _dbContext.Database.BeginTransaction();
            var newApiEndpoint = new ApiEndpoint()
            {
                Path = input.Path,
                Description = input.Description
            };
            _dbContext.ApiEndpoints.Add(newApiEndpoint);
            _dbContext.SaveChanges();



            var listNewKeyPermission = input.PermissionKeys.Select(pk => new KeyPermission
            {
                PermissionKey = pk.PermissionKey,
                PermissionLabel = pk.PermissionLabel,
                ParentId = pk.ParentId,
                OrderPriority = pk.OrderPriority
            }).ToList();
            _dbContext.KeyPermission.AddRange(listNewKeyPermission);
            _dbContext.SaveChanges();

            var newPermissionForApi = listNewKeyPermission.Select(c => new PermissionForApiEndpoint
            {
                KeyPermissionId = c.Id,
                ApiEndpointId = newApiEndpoint.Id
            });

            _dbContext.PermissionForApiEndpoints.AddRange(newPermissionForApi);
            _dbContext.SaveChanges();

            transaction.Commit();
        }

        public void UpdatePermissionConfig(UpdatePermissionConfigDto input)
        {
            _logger.LogInformation($"{nameof(UpdatePermissionConfig)}: input = {JsonSerializer.Serialize(input)}");
            using var transaction = _dbContext.Database.BeginTransaction();

            // Lấy ApiEndpoint cần cập nhật
            var apiEndpoint = _dbContext.ApiEndpoints.FirstOrDefault(a => a.Id == input.Id)
                                ?? throw new UserFriendlyException(ErrorCode.KeyPermissionNotFound);
            apiEndpoint.Path = input.Path;

            // Lấy danh sách KeyPermission theo Id của chúng
            var existingKeyPermissions = _dbContext.KeyPermission
                .Where(kp => input.PermissionKeys.Select(pk => pk.Id).Contains(kp.Id))
                .ToList();

            // Cập nhật hoặc thêm KeyPermission
            foreach (var permissionKey in input.PermissionKeys)
            {
                var pk = existingKeyPermissions.FirstOrDefault(kp => kp.Id == permissionKey.Id);
                if (pk != null)
                {
                    // Cập nhật nếu đã tồn tại
                    pk.PermissionLabel = permissionKey.PermissionLabel;
                    pk.PermissionKey = permissionKey.PermissionKey;
                    pk.OrderPriority = permissionKey.OrderPriority;
                    pk.ParentId = permissionKey.ParentId;
                }
                else
                {
                    // Thêm mới nếu chưa tồn tại
                    _dbContext.KeyPermission.Add(new KeyPermission
                    {
                        ParentId = permissionKey.ParentId,
                        OrderPriority = permissionKey.OrderPriority,
                        PermissionKey = permissionKey.PermissionKey,
                        PermissionLabel = permissionKey.PermissionLabel
                    });
                }
            }

            // Lấy danh sách PermissionForApiEndpoints liên quan
            var existingPermissionsForApi = _dbContext.PermissionForApiEndpoints
                .Where(pfa => input.PermissionKeys.Select(pk => pk.Id).Contains(pfa.KeyPermissionId)
                              && pfa.ApiEndpointId == input.Id)
                .ToList();

            // Cập nhật hoặc thêm PermissionForApiEndpoints
            foreach (var permissionKey in input.PermissionKeys)
            {
                var permissionForApi = existingPermissionsForApi
                    .FirstOrDefault(pfa => pfa.KeyPermissionId == permissionKey.Id);

                if (permissionForApi != null)
                {
                    // Cập nhật nếu đã tồn tại
                    permissionForApi.KeyPermissionId = permissionKey.Id;
                }
                else
                {
                    // Thêm mới nếu chưa tồn tại
                    _dbContext.PermissionForApiEndpoints.Add(new PermissionForApiEndpoint
                    {
                        ApiEndpointId = apiEndpoint.Id,
                        KeyPermissionId = permissionKey.Id,
                    });
                }
            }

            // Lưu thay đổi một lần duy nhất
            _dbContext.SaveChanges();
            transaction.Commit();
        }

        public PagingResult<PermissionApiDto> GetAllPermissionApi(PermissionApiRequestDto input)
        {
            var query = _dbContext.ApiEndpoints.Select(c => new PermissionApiDto
            {
                Id = c.Id,
                Path = c.Path,
                Description = c.Description,
            });

            var result = new PagingResult<PermissionApiDto>()
            {
                TotalItems = query.Count(),
            };

            if(input.PageSize != -1)
            {
                query = query.Skip(input.GetSkip()).Take(input.PageSize);
            }

            result.Items = query;
           
            return result;
        }

        public PermissionApiDetailDto GetPermissionApiById(int id)
        {
            var query = _dbContext.ApiEndpoints.Where(c => c.Id == id).Include(c => c.PermissionForApiEndpoints)
                                                    .ThenInclude(c => c.KeyPermission)
                                                    .Select(api => new PermissionApiDetailDto
                                                     {
                                                         Id = api.Id,
                                                         Path = api.Path,
                                                         Description = api.Description,
                                                         KeyPermissions = api.PermissionForApiEndpoints.Select(permissionForApi => new KeyPermissionDto
                                                         {
                                                             Id = permissionForApi.KeyPermission.Id,
                                                             PermissionKey = permissionForApi.KeyPermission.PermissionKey,
                                                             PermissionLabel = permissionForApi.KeyPermission.PermissionLabel,
                                                             ParentId = permissionForApi.KeyPermission.ParentId,
                                                             OrderPriority = permissionForApi.KeyPermission.OrderPriority
                                                         }).ToList()
                                                     }).FirstOrDefault() ?? throw new UserFriendlyException(ErrorCode.ApiEndpointNotFound);
            var result = (from api in _dbContext.ApiEndpoints
                          where api.Id == id
                          select new PermissionApiDetailDto
                          {
                              Id = api.Id,
                              Path = api.Path,
                              Description= api.Description,
                              KeyPermissions = (from permissionOfApi in _dbContext.PermissionForApiEndpoints
                                                join keyPermission in _dbContext.KeyPermission on permissionOfApi.KeyPermissionId equals keyPermission.Id
                                                where permissionOfApi.ApiEndpointId == api.Id
                                                select new KeyPermissionDto
                                                {
                                                    Id = keyPermission.Id,
                                                    PermissionKey = keyPermission.PermissionKey,
                                                    PermissionLabel = keyPermission.PermissionLabel,
                                                    ParentId = keyPermission.ParentId,
                                                    OrderPriority = keyPermission.OrderPriority
                                                }).ToList()
                          }).FirstOrDefault() ?? throw new UserFriendlyException(ErrorCode.ApiEndpointNotFound);
            return result;
        }
    }
}
