namespace RolePermissionDemo.Shared.Consts.Permissions
{
    public static class PermissionKeys
    {
        public const string App = "app";
        #region dashboard
        public const string MenuDashboard = "menu_dashboard";
        #endregion

        #region user account
        public const string MenuUserAccount = "menu_user_account";
        public const string CreateUserAccount = "button_create_user_account";
        public const string ListUserAccount = "table_create_user_account";
        #endregion
    }

    public static class PermissionLabel
    {
        public const string App = "App permission";
        #region dashboard
        public const string MenuDashboard = "Tổng quan";
        #endregion

        #region user account
        public const string MenuUserAccount = "Quản lý tài khoản";
        public const string CreateUserAccount = "Thêm mới";
        public const string ListUserAccount = "Danh sách";
        #endregion
    }
}