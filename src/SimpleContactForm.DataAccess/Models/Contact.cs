using SimpleContactForm.DataAccess.Data;

namespace SimpleContactForm.DataAccess.Models;

public class Contact
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public Category Category { get; set; }
    public DateTime EmploymentDate { get; set; } 

    public List<Skill>? Skills { get; set; } 
    public int SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }

    public Contact()
    {
    }

    public Contact(string firstName, string lastName, string email, Category category, DateTime? employmentDate = null, 
        List<Skill>? skills = null, Specialization? specialization = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Category = category;
        EmploymentDate = employmentDate ?? DateTime.UtcNow;
        Skills = skills;
        Specialization = specialization;
    }
}