using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RolePermissionDemo.Domains.Entities;
using RolePermissionDemo.Domains.Entities.Product;
using RolePermissionDemo.Domains.EntityBase;
using RolePermissionDemo.Shared.Consts.Product;
using System.Security.Claims;

namespace RolePermissionDemo.Infrastructures.Persistances
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly int? UserId = null;

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        #endregion

        #region Product
            public DbSet<BillCategory> BillCategories {  get; set; } 
            public DbSet<Provider> Providers {  get; set; }
            public DbSet<AssetProvider> AssetProviders {  get; set; }
            public DbSet<Bill> Bills {  get; set; }
            public DbSet<Transaction> Transactions {  get; set; }   

        #endregion
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            var claims = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            var claim = claims?.FindFirst("user_id");
            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                UserId = userId;
            }
        }

        private void CheckAudit()
        {
            ChangeTracker.DetectChanges();
            var added = ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Added)
                .Select(t => t.Entity)
                .AsParallel();

            added.ForAll(entity =>
            {
                if (entity is ICreatedBy createdEntity && createdEntity.CreatedBy == null)
                {
                    createdEntity.CreatedDate = DateTime.Now;
                    createdEntity.CreatedBy = UserId;
                }
            });

            var modified = ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Modified)
                        .Select(t => t.Entity)
                        .AsParallel();
            modified.ForAll(entity =>
            {
                if (entity is IModifiedBy modifiedEntity && modifiedEntity.ModifiedBy == null)
                {
                    modifiedEntity.ModifiedDate = DateTime.Now;
                    modifiedEntity.ModifiedBy = UserId;
                }
            });
        }

        public override int SaveChanges()
        {
            CheckAudit();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            CheckAudit();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            CheckAudit();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            CheckAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var entity in entityTypes)
            {
                if (entity.ClrType.IsAssignableTo(typeof(ICreatedBy)))
                {
                }
            }

            #region User
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(i => i.Id);
                e.Property(c => c.CreatedDate).HasDefaultValue(DateTime.Now);
                e.HasMany(u => u.UserRoles).WithOne(c => c.User).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade); ;

            });
            #endregion

            #region UserRole
            modelBuilder.Entity<UserRole>(e =>
            {
                e.HasKey(ur => ur.Id);
                e.Property(c => c.CreatedDate).HasDefaultValue(DateTime.Now);
                e.Property(ur => ur.Deleted).HasDefaultValue(false);
            });
            #endregion

            #region  Role
            modelBuilder.Entity<Role>(e =>
            {
                e.HasKey(r => r.Id);
                e.Property(r => r.CreatedDate).HasDefaultValue(DateTime.Now);

                e.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(r => r.RolePermissions)
                .WithOne(rp => rp.Role)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

                e.Property(r => r.Deleted).HasDefaultValue(false);
            });
            #endregion

            #region RolePermission
            modelBuilder.Entity<RolePermission>(e =>
            {
                e.HasKey(rp => rp.Id);

                e.Property(rp => rp.Deleted).HasDefaultValue(false);
            });
            #endregion

            #region Product
            //Bill category
            modelBuilder.Entity<BillCategory>(e =>
            {
                e.HasKey(bc => bc.Id);
                e.HasMany(bc => bc.Providers).WithOne(p => p.BillCategory).HasForeignKey(p => p.BillCategoryId);
                e.HasMany(bc => bc.Bills).WithOne(p => p.BillCategory).HasForeignKey(p => p.BillCategoryId);
            });

            // Provider
            modelBuilder.Entity<Provider>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasMany(p => p.AssetProviders).WithOne(ap => ap.Provider).HasForeignKey(ap => ap.ProviderId);
            });

            // Asset provider
            modelBuilder.Entity<AssetProvider>(e =>
            {
                e.HasKey(p => p.Id);
            });

            var jsonConverter = new ValueConverter<string, string>(v => v, v => v);
            //Bill
            modelBuilder.Entity<Bill>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(b => b.BillDetail).HasConversion(jsonConverter);
            });
            modelBuilder.Entity<Transaction>(e =>
            {
                e.HasKey(t => t.Id);
                e.Property(t => t.Status).HasDefaultValue(TransactionStatus.INIT);
                e.Property(t => t.TransactionCode).HasDefaultValue(new DateTime().ToString("ssmmHHddMMyyyyfff"));
            });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
