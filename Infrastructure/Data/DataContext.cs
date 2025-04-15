using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{

    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TaskAssigment> TaskAssigments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasMany(n => n.Tasks)
            .WithOne(n => n.Project)
            .HasForeignKey(n => n.ProjectId);

        modelBuilder.Entity<User>()
            .HasMany(n => n.Tasks)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserId);

        // modelBuilder.Entity<TaskAssigment>()
        //     .HasOne(n => n.Task)
        //     .WithMany(n => n.)
        //     .HasForeignKey(n=>n.)
        // Нафахмидумша
    }
}
