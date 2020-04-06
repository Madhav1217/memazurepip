using Microsoft.EntityFrameworkCore;
using Aliera.DatabaseEntities.AuditLogModels;

namespace Aliera.DatabaseEntities
{
    public partial class AuditLogContext : DbContext
    {
        //leave this commented for context pooling
        //public AuditLogContext()
        //{
        //}

        public AuditLogContext(DbContextOptions<AuditLogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditLogs> AuditLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLogs>(entity =>
            {
                entity.HasKey(e => e.AuditLogId)
                    .HasName("PK_AuditLogs_AuditLogId");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.Property(e => e.Comments).HasMaxLength(1000);

                entity.Property(e => e.EntityType).HasMaxLength(20);

                entity.Property(e => e.UserIpaddress)
                    .HasColumnName("UserIPAddress")
                    .HasMaxLength(30);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
