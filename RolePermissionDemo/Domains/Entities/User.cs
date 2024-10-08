﻿using Microsoft.EntityFrameworkCore;
using RolePermissionDemo.Domains.EntityBase;
using RolePermissionDemo.Domains.Schemas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RolePermissionDemo.Shared.Consts;

namespace RolePermissionDemo.Domains.Entities
{
    [Table(nameof(User), Schema = DbSchemas.Auth)]
    [Index(nameof(Deleted), nameof(Status), Name = $"IX_{nameof(User)}")]
    public class User : IFullAudited
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Username { get; set; } = null!;
        [MaxLength(512)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Trạng thái
        /// <see cref="CommonStatus"/>
        /// </summary>
        public int Status {  get; set; }
        public int UserType {  get; set; }

        public List<UserRole> UserRoles { get; set; } = new();
        #region audit
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        #endregion
    }
}
