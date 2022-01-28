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
    public DbSet<Group> Groups { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<WikiCategory> WikiCategories { get; set; }
    public DbSet<WikiArticle> WikiArticles { get; set; }
    public DbSet<Attachment> Attachments { get; set; }

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
        modelBuilder.Entity<Member>().HasKey(m => new { m.UserId, m.GroupId, m.ProjectId });
        modelBuilder.Entity<Comment>().HasKey(c => new { c.CommentedId, c.Type });
        modelBuilder.Entity<Attachment>().HasKey(a => new { a.AttachedId, a.Type });

        modelBuilder.Entity<User>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Issue>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Project>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Document>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Customer>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Contact>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Deal>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Comment>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<TimeEntry>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Member>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<WikiArticle>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
        modelBuilder.Entity<Attachment>().Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");

        modelBuilder.Entity<Group>()
            .Property(p => p.Permissions)
            .HasConversion<StringListConverter, StringListComparer>();

        modelBuilder.Entity<WikiCategory>()
            .Property(p => p.Tags)
            .HasConversion<StringListConverter, StringListComparer>();

        modelBuilder.Entity<WikiArticle>()
            .Property(p => p.Tags)
            .HasConversion<StringListConverter, StringListComparer>();
    }
}
