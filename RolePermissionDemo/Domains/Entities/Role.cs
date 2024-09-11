using Microsoft.EntityFrameworkCore;
using RolePermissionDemo.Domains.EntityBase;
using RolePermissionDemo.Domains.Schemas;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RolePermissionDemo.Shared.Consts;

namespace RolePermissionDemo.Domains.Entities
{
    [Table(nameof(Role), Schema = DbSchemas.Auth)]
    [Index(nameof(Deleted), nameof(Name), nameof(Status), Name = $"IX_{nameof(Role)}")]
    public class Role : IFullAudited
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        /// <summary>
        /// Trạng thái role
        /// <see cref="CommonStatus"/>
        /// </summary>
        public int Status { get; set; }
        public List<UserRole> UserRoles { get; set; } = new();
        public List<RolePermission> RolePermissions { get; set; } = new();
        #region audit
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
