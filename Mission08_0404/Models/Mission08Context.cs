using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Mission08_0404.Models;

public partial class Mission08Context : DbContext
{
    public Mission08Context()
    {
    }

    public Mission08Context(DbContextOptions<Mission08Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Quadrant> Quadrants { get; set; }

    public virtual DbSet<TaskItem> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=mission_08.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId);

            entity.ToTable("Category");

            entity.HasIndex(e => e.CatName, "IX_Category_CatName").IsUnique();

            entity.Property(e => e.CatId).HasColumnName("CatID");
        });

        modelBuilder.Entity<Quadrant>(entity =>
        {
            entity.HasKey(e => e.QuadId);

            entity.ToTable("Quadrant");

            entity.Property(e => e.QuadId).HasColumnName("QuadID");
        });

        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("TaskItem");

            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.QuadrantId).HasColumnName("QuadrantID");

            entity.HasOne(d => d.Cat).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Quadrant).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.QuadrantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
