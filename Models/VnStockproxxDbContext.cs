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
            entity.ToTable("Post", tb =>
                {
                    tb.HasTrigger("Post_ForUpdate");
                    tb.HasTrigger("trg_UpdateUpdatedDate");
                });

            entity.HasIndex(e => e.CateId, "IX_Post_CateId");

            entity.Property(e => e.Content)
                .HasMaxLength(299)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("content");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.Image).HasMaxLength(299);
            entity.Property(e => e.Teaser)
                .HasMaxLength(299)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("teaser");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .UseCollation("Vietnamese_CI_AS")
                .HasColumnName("title");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedDate");
            entity.Property(e => e.ViewCount)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("viewCount");

            entity.HasOne(d => d.Cate).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Post_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
