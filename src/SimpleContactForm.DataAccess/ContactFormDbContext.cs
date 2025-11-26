using Microsoft.EntityFrameworkCore;
using SimpleContactForm.DataAccess.Data;
using SimpleContactForm.DataAccess.Models;

namespace SimpleContactForm.DataAccess;

public sealed class ContactFormDbContext : DbContext
{
    public ContactFormDbContext(DbContextOptions<ContactFormDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Skill> Skills { get; set; }

    public DbSet<Specialization> Specializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasMany(c => c.Skills)
            .WithMany(s => s.Contacts);

        modelBuilder.Entity<Specialization>()
            .HasMany(s => s.Contacts)
            .WithOne(c => c.Specialization);

        var skillCSharp = new Skill { Id = 1, Name = "C#" };
        var skillAspNet = new Skill { Id = 2, Name = "ASP.NET" };
        var skillReact = new Skill { Id = 3, Name = "React" };

        modelBuilder.Entity<Skill>().HasData(
            skillCSharp,
            skillAspNet,
            skillReact,
            new Skill { Id = 4, Name = "C++" },
            new Skill { Id = 5, Name = "DDD" },
            new Skill { Id = 6, Name = "TS" },
            new Skill { Id = 7, Name = "JS" },
            new Skill { Id = 8, Name = "Vue.js" }
        );

        var specBackend = new Specialization { Id = 4, Name = "Backend Developer", Code = "dev12_12" };
        var specFront = new Specialization { Id = 3, Name = "Frontend Developer", Code = "dev90_12" };

        modelBuilder.Entity<Specialization>().HasData(
            new Specialization { Id = 1, Name = "Mobile Developer", Code = "mob_1" },
            new Specialization { Id = 2, Name = "Data Engineer", Code = "data_12_2" },
            specFront,
            specBackend
        );

        modelBuilder.Entity<Contact>().HasData(new Contact
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Smith",
            Email = "john@example.com",
            Category = Category.Junior,
            EmploymentDate = DateTime.Now - TimeSpan.FromDays(1000),
            Skills =
            [
                skillCSharp,
                skillAspNet
            ],
            Specialization = specBackend
        }, new Contact
        {
            Id = Guid.NewGuid(),
            FirstName = "Sarah",
            LastName = "Johnson",
            Email = "sarah@example.com",
            Category = Category.Senior,
            EmploymentDate = DateTime.Now - TimeSpan.FromDays(5000),
            Skills =
            [
                skillCSharp,
                skillAspNet
            ],
            Specialization = specBackend
        }, new Contact
        {
            Id = Guid.NewGuid(),
            FirstName = "Michael",
            LastName = "Brown",
            Email = "michael@example.com",
            Category = Category.Middle,
            EmploymentDate = DateTime.Now - TimeSpan.FromDays(6),
            Skills = [skillReact],
            Specialization = specFront
        });
    }
}