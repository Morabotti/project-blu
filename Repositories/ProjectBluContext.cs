using ProjectBlu.Models;

namespace ProjectBlu.Repositories;

public class ProjectBluContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Deal> Deals { get; set; }

    public ProjectBluContext(DbContextOptions<ProjectBluContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Issue>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Project>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Document>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Customer>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Contact>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Deal>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getdate()");
    }
}
