using SimpleContactForm.DataAccess.Data;
using SimpleContactForm.DataAccess.Models;

namespace SimpleContactForm.Abstractions.Dtos;

public record ContactDto(Guid Id, string FullName, string Email, Category Category, DateTime EmploymentDate, List<int>? SkillIds = null, int? SpecializationId = null);

public record CreateContactDto(Guid Id, string FirstName, string LastName, string Email, Category Category, DateTime EmploymentDate, List<int>? SkillIds = null, int? SpecializationId = null);

public static class ContactExtension
{
    public static ContactDto ToContactDto(this Contact contact)
    {
        return new ContactDto(contact.Id,
            $"{contact.FirstName} {contact.LastName}",
            contact.Email,
            contact.Category,
            contact.EmploymentDate,
            contact.Skills?.Select(x => x.Id).ToList(),
            contact.Specialization?.Id);
    }
}