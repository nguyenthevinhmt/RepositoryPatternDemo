using Microsoft.EntityFrameworkCore;
using RolePermissionDemo.Domains.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolePermissionDemo.Domains.Entities.Product
{
    [Table(nameof(AssetProvider))]

    [Index(nameof(ProviderId), nameof(Deleted), Name = $"IX_{nameof(AssetProvider)}", IsUnique = false)]
    public class AssetProvider : IFullAudited
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [MaxLength(512)]
        public string? Description { get; set; }
        public int ProviderId {  get; set; }
        public Provider Provider { get; set; } = null!;

        #region audit
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
