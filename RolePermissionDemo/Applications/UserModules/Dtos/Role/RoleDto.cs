namespace RolePermissionDemo.Applications.UserModules.Dtos.Role
{
    public class RoleDto
    {
        public string Name { get; set; } = null!;
        /// <summary>
        /// Trạng thái role
        /// <see cref="CommonStatus"/>
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Số người sử dụng
        /// </summary>
        //public int CountUserUsing {  get; set; }

    }
}
