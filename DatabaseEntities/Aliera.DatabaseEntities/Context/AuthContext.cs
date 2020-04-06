using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aliera.DatabaseEntities.Models.Auth
{
    public partial class AuthContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, long>
    {
        //public AuthContext()
        //{
        //}

        public AuthContext(DbContextOptions<AuthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PermissionDefinition> PermissionDefinition { get; set; }
        public virtual DbSet<PermissionGroup> PermissionGroup { get; set; }
        public virtual DbSet<PortalRoleTypes> PortalRoleTypes { get; set; }
        public virtual DbSet<PortalRoles> PortalRoles { get; set; }
        public virtual DbSet<Portals> Portals { get; set; }        
        public virtual DbSet<RolePermission> RolePermission { get; set; }        
        public virtual DbSet<Tokens> Tokens { get; set; }        
        public virtual DbSet<UserPermission> UserPermission { get; set; }
        public virtual DbSet<UserRoleAssociation> UserRoleAssociation { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.EmailConfirmed).HasColumnName("IsEmailVerified");

            modelBuilder.Entity<PermissionDefinition>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.PermissionGroup)
                    .WithMany(p => p.PermissionDefinition)
                    .HasForeignKey(d => d.PermissionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PermissionGroup>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<PortalRoleTypes>(entity =>
            {
                entity.HasKey(e => e.PortalRoleTypeId)
                    .HasName("PK_PortalRoleTypes_PortalRoleTypeId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PortalRoles>(entity =>
            {
                entity.HasKey(e => e.PortalRoleId)
                    .HasName("PK_PortalRoles_PortalRoleId");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.PortalRoleType)
                    .WithMany(p => p.PortalRoles)
                    .HasForeignKey(d => d.PortalRoleTypeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Portals>(entity =>
            {
                entity.HasKey(e => e.PortalId)
                    .HasName("PK_Portals_PortalId");
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.PermissionDefinition)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.PermissionDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PortalRole)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.PortalRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermission_PortalRoleId");
            });            

            modelBuilder.Entity<Tokens>(entity =>
            {
                entity.HasIndex(e => e.UserId);
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.PermissionDefinition)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.PermissionDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserPermission_permissiondefinition_fk");

                entity.HasOne(d => d.UserRoleAssociation)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.UserRoleAssociationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserPermission_UserRoleAssociationId_fk");
            });

            modelBuilder.Entity<UserRoleAssociation>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.UserId, e.IsCustomPermission });

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.PortalRole)
                    .WithMany(p => p.UserRoleAssociation)
                    .HasForeignKey(d => d.PortalRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoleAssociation_PortalRoleId");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.UserRoleAssociation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoleAssociation_UserId");
            });
        }
    }
}
