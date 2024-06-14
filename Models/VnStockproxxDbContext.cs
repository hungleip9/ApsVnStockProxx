using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VnStockproxx.Models;

public partial class VnStockproxxDbContext : DbContext
{
    public VnStockproxxDbContext()
    {
    }

    public VnStockproxxDbContext(DbContextOptions<VnStockproxxDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Post> Post { get; set; }

    public virtual DbSet<Tag> Tag { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=VnStockproxx;User Id=sa;Password=abc123456;Trusted_Connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.NameMap).HasMaxLength(100);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasIndex(e => e.CateId, "IX_Post_CateId");

            entity.Property(e => e.Content).UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.CreatedBy).HasMaxLength(299);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(299);
            entity.Property(e => e.ImageContent)
                .HasMaxLength(299)
                .UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.Title).UseCollation("Vietnamese_CI_AS");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Cate).WithMany(p => p.Post)
                .HasForeignKey(d => d.CateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Post_Category");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasMany(d => d.IdPost).WithMany(p => p.IdTag)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",
                    r => r.HasOne<Post>().WithMany()
                        .HasForeignKey("IdPost")
                        .HasConstraintName("FK_PostTag_Post"),
                    l => l.HasOne<Tag>().WithMany()
                        .HasForeignKey("IdTag")
                        .HasConstraintName("FK_PostTag_Tag"),
                    j =>
                    {
                        j.HasKey("IdTag", "IdPost");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
