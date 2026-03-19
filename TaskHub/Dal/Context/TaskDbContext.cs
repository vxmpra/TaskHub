using System;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Context;

public sealed class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            
            entity.Property(x => x.Title).HasMaxLength(500);
            
            entity.Property(x => x.CreatedUtc).HasDefaultValueSql("now()");
            
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Ignore<User>();

        base.OnModelCreating(modelBuilder);
    }
}