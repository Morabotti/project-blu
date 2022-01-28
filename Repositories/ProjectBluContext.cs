using ProjectBlu.Models;
using ProjectBlu.Repositories.Converters;

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
    public DbSet<IssueCategory> IssueCategories { get; set; }
    public DbSet<IssueStatus> IssueStatuses { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<TimeEntry> TimeEntries { get; set; }

    public ProjectBluContext(DbContextOptions<ProjectBluContext> options) : base(options)
    {

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
                .HaveColumnType("date");

        builder.Properties<DateOnly?>()
                .HaveConversion<NullableDateOnlyConverter, NullableDateOnlyComparer>()
                .HaveColumnType("date");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Issue>().Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Project>().Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Document>().Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Customer>().Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Contact>().Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Deal>().Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Comment>().Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
        modelBuilder.Entity<TimeEntry>().Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
    }
}
