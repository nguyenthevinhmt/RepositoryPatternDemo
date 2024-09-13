namespace RolePermissionDemo.Shared.Consts.Product
{
    /// <summary>
    /// Trạng thái giao dịch
    /// </summary>
    public static class TransactionStatus
    {
        public const int INIT = 1; //Khởi tạo
        public const int PENDING = 2; // Đang xử lý
        public const int SUCCESS = 3; // Thành công
        public const int FAILED = 4; // Thất bại
    }
}
