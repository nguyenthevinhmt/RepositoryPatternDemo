namespace RolePermissionDemo.Applications.UserModules.Dtos
{
    public class TokenApiDto
    {
        public string AccessToken { get; set; } = null!;
        public string? RefreshToken { get; set; }
    }
}
