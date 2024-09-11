using RolePermissionDemo.Domains.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolePermissionDemo.Domains.Entities.Product
{
    /// <summary>
    /// Loại hóa đơn cho sản phẩm nào
    /// </summary>
    [Table(nameof(BillCategory))]
    public class BillCategory : IFullAudited
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(512)]
        public string? Description { get; set; }
        public List<Product>? Products { get; set; }
        public List<Bill>? Bills { get; set; }
        #region audit
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
