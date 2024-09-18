﻿using RolePermissionDemo.Domains.Schemas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolePermissionDemo.Domains.Entities
{
    [Table(nameof(ApiEndpoint), Schema = DbSchemas.Auth)]
    public class ApiEndpoint
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Path { get; set; } = null!;
    }
}
