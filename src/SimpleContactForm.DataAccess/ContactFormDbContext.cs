using Microsoft.EntityFrameworkCore;
using SimpleContactForm.DataAccess.Models;

namespace SimpleContactForm.DataAccess;

public sealed class ContactFormDbContext : DbContext
{
    public ContactFormDbContext(DbContextOptions<ContactFormDbContext> options) : base(options)
    {
        var a = 12;
        // Database.EnsureDeleted();
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

        modelBuilder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = "C#" },
            new Skill { Id = 2, Name = "C++" },
            new Skill { Id = 3, Name = "ASP.NET" },
            new Skill { Id = 4, Name = "DDD" },
            new Skill { Id = 5, Name = "React" },
            new Skill { Id = 6, Name = "TS" },
            new Skill { Id = 7, Name = "JS" },
            new Skill { Id = 8, Name = "Vue.js" }
        );
        
        modelBuilder.Entity<Specialization>().HasData(
            new Specialization { Id = 1, Name = "Mobile Developer", Code = "mob_1" },
            new Specialization { Id = 2, Name = "Data Engineer", Code = "data_12_2" },
            new Specialization { Id = 3, Name = "Frontend Developer", Code = "dev90_12" },
            new Specialization { Id = 4, Name = "Backend Developer", Code = "dev12_12" }
        );
    }
}