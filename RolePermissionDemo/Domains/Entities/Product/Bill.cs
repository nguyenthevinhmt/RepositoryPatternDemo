using RolePermissionDemo.Domains.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolePermissionDemo.Domains.Entities.Product
{
    [Table(nameof(Bill))]
    public class Bill : IFullAudited
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(512)]
        public string? Description { get; set; }
        public int ProviderId {  get; set; }
        public Provider Provider { get; set; } = null!;

        public int ProductId {  get; set; }
        public Product Product { get; set; } = null!;

        public string BillDetail { get; set; } = null!;

        #region audit
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
