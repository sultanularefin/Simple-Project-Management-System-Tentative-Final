namespace Smpl_prjct_mng_mnt_tol.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EF3 : DbContext
    {
        public EF3()
            : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }


        public virtual DbSet<AllFile> AllFiles { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<Designation>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Designation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Priority>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Priority)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.AllFiles)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Assignments)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectStatus>()
                .HasMany(e => e.Projects)
                .WithOptional(e => e.ProjectStatus)
                .HasForeignKey(e => e.statusId);

            modelBuilder.Entity<Task>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Assignments)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.commenterid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.assigneeid)
                .WillCascadeOnDelete(false);
        }
    }
}