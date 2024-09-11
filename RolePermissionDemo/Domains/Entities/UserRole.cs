﻿using Microsoft.EntityFrameworkCore;
using RolePermissionDemo.Domains.EntityBase;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RolePermissionDemo.Domains.Schemas;

namespace RolePermissionDemo.Domains.Entities
{
    [Table(nameof(UserRole), Schema = DbSchemas.Auth)]
    [Index(nameof(Deleted), nameof(UserId), nameof(RoleId), Name = $"IX_{nameof(UserRole)}")]
    public class UserRole : IFullAudited
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        #region audit
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
