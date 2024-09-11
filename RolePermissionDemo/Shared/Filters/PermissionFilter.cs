using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RolePermissionDemo.Applications.UserModules.Abstracts;
using RolePermissionDemo.Applications.UserModules.Implements;
using RolePermissionDemo.Shared.ApplicationBase.Common;
using RolePermissionDemo.Shared.Consts;
using RolePermissionDemo.Shared.Consts.Exceptions;

namespace RolePermissionDemo.Shared.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionFilterAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _permissions;
        private IPermissionServices? _permissionServices;
        private IHttpContextAccessor? _httpContext;

        public PermissionFilterAttribute(params string[] permissions)
        {
            _permissions = permissions;
        }

        private void GetServices(AuthorizationFilterContext context)
        {
            _permissionServices =
                context.HttpContext.RequestServices.GetRequiredService<IPermissionServices>();
            _httpContext =
                context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            GetServices(context);
            var userType = _httpContext!.GetCurrentUserType();
            if (userType == UserTypes.ADMIN)
            {
                return;
            }

            bool isGrant = _permissionServices!.CheckPermission(_permissions);
            var permissionQueryParam = context
               .HttpContext.Request.Query[QueryParamKeys.Permission]
               .ToString()
               .Trim();
            if (
                !string.IsNullOrEmpty(permissionQueryParam)
                && isGrant
                && !_permissionServices!.CheckPermission(permissionQueryParam)
                && _permissions.Contains(permissionQueryParam)
            )
            {
                isGrant = false;
            }

            if (!isGrant) {
                context.Result = new UnauthorizedObjectResult(
                    new { message = ErrorMessage.UserNotHavePermission }
                );
            }
        }
    }
}
