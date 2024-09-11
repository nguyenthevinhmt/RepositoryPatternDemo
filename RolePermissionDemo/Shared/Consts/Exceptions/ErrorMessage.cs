namespace RolePermissionDemo.Shared.Consts.Exceptions
{
    public static class ErrorMessage
    {
        public const string System = "Lỗi hệ thống";
        public const string BadRequest = "BadRequest";
        public const string Unauthorized = "Unauthorized";
        public const string NotFound = "NotFound";
        public const string InternalServerError = "InternalServerError";


        public const string UserNotFound = "Không tìm thấy người dùng";
        public const string PasswordWrong = "Mật khẩu sai";
        public const string UsernameIsExist = "Tên người dùng đã tồn tại trong hệ thống";
        public const string PasswordMustBeLongerThanSixCharacter = "Mật khẩu phải dài hơn 6 kí tự";
        public const string TypeofPasswordMustBeNumberOrString = "Mật khẩu phải thuộc kiểu số hoặc chữ";
        public const string PasswordMustBeContainsSpecifyCharacter = "Mật khẩu phải chứa ít nhất 1 kí tự đặc biết";
        public const string LoginExpired = "Hết hạn đăng nhập, vui lòng đăng nhập lại";
        public const string RoleNotFound = "Quyền không tồn tại";
        public const string UserNotHavePermission = "Tài khoản không có quyền truy cập!";
        public const string RoleOrUserNotFound = "Role tài khoản không tồn tại";


    }
}
