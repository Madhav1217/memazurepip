using Microsoft.EntityFrameworkCore;
using Aliera.DatabaseEntities.DMSModels;

namespace Aliera.DatabaseEntities
{
    public partial class DMSContext : DbContext
    {
        //leave this commented for context pooling
        //public DMSContext()
        //{
        //}

        public DMSContext(DbContextOptions<DMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DocumentAccess> DocumentAccess { get; set; }
        public virtual DbSet<DocumentStatus> DocumentStatus { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentAccess>(entity =>
            {
                entity.HasKey(e => new { e.DocumentId, e.EntityTypeId, e.EntityId });

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");

                entity.Property(e => e.EntityId).HasColumnName("EntityID");
            });

            modelBuilder.Entity<DocumentStatus>(entity =>
            {
                entity.Property(e => e.DocumentStatusId)
                    .HasColumnName("DocumentStatusID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.Property(e => e.DocumentTypeId)
                    .HasColumnName("DocumentTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Documents>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.DocumentCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocumentId123)
                    .IsRequired()
                    .HasColumnName("DocumentID_123")
                    .HasMaxLength(10);

                entity.Property(e => e.DocumentStatusId).HasColumnName("DocumentStatusID");

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.FileType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.IsUploadedS3123).HasColumnName("isUploadedS3_123");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(3000);

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.OwnerTypeId).HasColumnName("OwnerTypeID");

                entity.Property(e => e.Url123)
                    .IsRequired()
                    .HasColumnName("URL_123")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });
        }
    }
}
