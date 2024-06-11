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

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=VnStockproxx;User Id=sa;Password=abc123456;Trusted_Connection=false;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("categoryName");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.HasIndex(e => e.CateId, "IX_Post_CateId");

            entity.Property(e => e.Content)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("content");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(299)
                .HasColumnName("createdBy");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.Image).HasMaxLength(299);
            entity.Property(e => e.ImageContent)
                .HasMaxLength(299)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("imageContent");
            entity.Property(e => e.Tag)
                .HasMaxLength(299)
                .HasColumnName("tag");
            entity.Property(e => e.Title)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("title");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedDate");
            entity.Property(e => e.ViewCount).HasColumnName("viewCount");

            entity.HasOne(d => d.Cate).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Post_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
