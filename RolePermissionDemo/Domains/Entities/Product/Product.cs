using RolePermissionDemo.Domains.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolePermissionDemo.Domains.Entities.Product
{
    [Table(nameof(Product))]
    public class Product : IFullAudited
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!; 
        /// <summary>
        /// Mô tả
        /// </summary>
        [MaxLength(512)]  
        public string? Description { get; set; }
        public int BillCategoryId {  get; set; }
        public BillCategory BillCategory { get; set; } = null!;
        public List<Provider>? Providers {  get; set; } 
        

        #region audit
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
