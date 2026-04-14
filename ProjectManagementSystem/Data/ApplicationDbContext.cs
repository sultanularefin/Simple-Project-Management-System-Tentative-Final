using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<AllFile> AllFiles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Comment configuration
            modelBuilder.Entity<Comment>()
                .Property(e => e.comment)
                .IsUnicode(false);

            // Designation relationship
            modelBuilder.Entity<Designation>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Designation)
                .HasForeignKey(e => e.designationId)
                .OnDelete(DeleteBehavior.SetNull);

            // Priority relationship
            modelBuilder.Entity<Priority>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.Priority)
                .HasForeignKey(e => e.priorityId)
                .OnDelete(DeleteBehavior.SetNull);

            // Project configuration
            modelBuilder.Entity<Project>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.AllFiles)
                .WithOne(e => e.Project)
                .HasForeignKey(e => e.projectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Assignments)
                .WithOne(e => e.Project)
                .HasForeignKey(e => e.projectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.Project)
                .HasForeignKey(e => e.projectId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProjectStatus relationship
            modelBuilder.Entity<ProjectStatus>()
                .HasMany(e => e.Projects)
                .WithOne(e => e.ProjectStatus)
                .HasForeignKey(e => e.statusId);

            // Task configuration
            modelBuilder.Entity<Models.Task>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Models.Task>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Task)
                .HasForeignKey(e => e.taskId)
                .OnDelete(DeleteBehavior.Cascade);

            // User relationships
            modelBuilder.Entity<User>()
                .HasMany(e => e.Assignments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.userId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.commenterid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.assigneeid)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
