namespace RolePermissionDemo.Shared.Consts.Permissions
{
    public static class PermisisonConfig
    {
        public static readonly Dictionary<string, PermissisonContent> appConfigs = new()
        {
            //App
            //{ PermissionKeys.App, new(nameof(PermissionKeys.App), PermissionLabel.App)},
            //Dashboard
            { PermissionKeys.MenuDashboard, new(nameof(PermissionKeys.MenuDashboard), PermissionLabel.MenuDashboard)},

            //User account
            { PermissionKeys.MenuUserAccount, new(nameof(PermissionKeys.MenuUserAccount), PermissionLabel.MenuUserAccount)},
            { PermissionKeys.CreateUserAccount, new(nameof(PermissionKeys.CreateUserAccount), PermissionLabel.CreateUserAccount, PermissionKeys.MenuUserAccount)},
            { PermissionKeys.ListUserAccount, new(nameof(PermissionKeys.ListUserAccount), PermissionLabel.ListUserAccount,  PermissionKeys.MenuUserAccount)},
        };
    }
}
