﻿using Microsoft.AspNetCore.Mvc;
using RolePermissionDemo.Applications.UserModules.Abstracts;
using RolePermissionDemo.Applications.UserModules.Dtos.Role;
using RolePermissionDemo.Shared.ApplicationBase.Common;
using RolePermissionDemo.Shared.WebAPIBase;

namespace RolePermissionDemo.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ApiControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(ILogger<RoleController> logger, IRoleService roleService) : base(logger)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Danh sách quyền
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("find-all")]
        public async Task<ApiResponse> FindAll([FromQuery] PagingRequestBaseDto input)
        {
            try
            {
                return new(await _roleService.FindAll(input));
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Thông tin chi tiết quyền
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("find-by-id/{id}")]
        public async Task<ApiResponse> FindById(int id)
        {
            try
            {
                return new(await _roleService.FindById(id));
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Thêm mới quyền
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ApiResponse> Create([FromBody] CreateRoleDto input)
        {
            try
            {
                await _roleService.Create(input);
                return new();
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Cập nhật quyền
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ApiResponse> Update([FromBody] UpdateRoleDto input)
        {
            try
            {
                await _roleService.Update(input);
                return new();
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Xóa quyền
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _roleService.Delete(id);
                return new();
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }

        /// <summary>
        /// Khóa/Mở khóa nhóm quyền
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut("update-status")]
        public async Task<ApiResponse> UpdateStatus(int id, int status)
        {
            try
            {
                await _roleService.UpdateStatusRole(id, status);
                return new();
            }
            catch (Exception ex)
            {
                return OkException(ex);
            }
        }
    }
}
