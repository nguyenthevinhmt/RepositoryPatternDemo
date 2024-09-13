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

            { PermissionKeys.MenuConfig, new(nameof(PermissionKeys.MenuConfig), PermissionLabel.MenuConfig)},
            { PermissionKeys.MenuRoleManager, new(nameof(PermissionKeys.MenuRoleManager), PermissionLabel.MenuRoleManager, PermissionKeys.MenuConfig)},
            { PermissionKeys.ButtonCreateRole, new(nameof(PermissionKeys.ButtonCreateRole), PermissionLabel.ButtonCreateRole, PermissionKeys.MenuRoleManager)},
            { PermissionKeys.TableRole, new(nameof(PermissionKeys.TableRole), PermissionLabel.TableRole, PermissionKeys.MenuRoleManager)},
            { PermissionKeys.ButtonDetailRole, new(nameof(PermissionKeys.ButtonDetailRole), PermissionLabel.ButtonDetailRole, PermissionKeys.TableRole)},
            { PermissionKeys.ButtonUpdateRole, new(nameof(PermissionKeys.ButtonUpdateRole), PermissionLabel.ButtonUpdateRole, PermissionKeys.TableRole)},
           
            ////User account
            //{ PermissionKeys.MenuUserAccount, new(nameof(PermissionKeys.MenuUserAccount), PermissionLabel.MenuUserAccount)},
            //{ PermissionKeys.CreateUserAccount, new(nameof(PermissionKeys.CreateUserAccount), PermissionLabel.CreateUserAccount, PermissionKeys.MenuUserAccount)},
            //{ PermissionKeys.ListUserAccount, new(nameof(PermissionKeys.ListUserAccount), PermissionLabel.ListUserAccount,  PermissionKeys.MenuUserAccount)},
        };
    }
}
