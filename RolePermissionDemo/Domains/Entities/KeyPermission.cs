﻿using Microsoft.EntityFrameworkCore;
using RolePermissionDemo.Domains.EntityBase;
using RolePermissionDemo.Domains.Schemas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolePermissionDemo.Domains.Entities
{
    [Table(nameof(KeyPermission), Schema = DbSchemas.Auth)]
    [Index(nameof(ParentId), nameof(Deleted), nameof(OrderPriority), Name = $"IX_{nameof(KeyPermission)}")]
    public class KeyPermission : IFullAudited
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string PermissionKey { get; set; } = null!;
        [MaxLength(50)]
        public string? PermissionLabel { get; set; }
        public int? ParentId { get; set; }
        public int OrderPriority { get; set; }
        public List<KeyPermission>? Children { get; set; }

        public List<PermissionForApiEndpoint>? PermissionForApiEndpoints { get; set; }
        #region audit
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
