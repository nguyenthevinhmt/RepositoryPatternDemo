using Microsoft.EntityFrameworkCore;
using RolePermissionDemo.Domains.EntityBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolePermissionDemo.Domains.Entities.Product
{
    [Table(nameof(Transaction))]
    [Index(nameof(BillId), nameof(Deleted), Name = $"IX_{nameof(Transaction)}", IsUnique = false)]
    public class Transaction : IFullAudited
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(512)]
        public string? Description { get; set; }
        public int TransactionType { get; set; }
        public double TransactionAmount { get; set; }
        public double TransactionFee { get; set; }
        [MaxLength(50)]
        public string TransactionCode { get; set; } = null!;
        [MaxLength(20)]
        public string PaymentAccountNumber { get; set; } = null!;
        public DateTime ExpiredDate { get; set; }
        public int Status {  get; set; }
        public int BillId {  get; set; }
        public Bill Bill { get; set; } = null!;

        #region audit
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
