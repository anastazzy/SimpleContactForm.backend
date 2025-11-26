using Microsoft.EntityFrameworkCore;
using SimpleContactForm.Abstractions.Dtos;
using SimpleContactForm.Abstractions.Interfaces;
using SimpleContactForm.DataAccess;
using SimpleContactForm.DataAccess.Data;
using SimpleContactForm.DataAccess.Models;
using SimpleContactForm.Utils.Utils;

namespace SimpleContactForm.Service;

public class ContactService : IContactService
{
    private readonly ContactFormDbContext _context;
    private const string ErrorWhenDateEarlyToday = "The date of employment cannot be earlier than today's date";
    private const string ErrorWhenCategoryDontExist = "The employee must be assigned a category";
    private const string ErrorWhenSearchStringIsShort = "The search is possible if more than 2 characters are entered.";

    public ContactService(ContactFormDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateContactAsync(CreateContactDto dto)
    {
        if (dto.EmploymentDate > DateTime.UtcNow)
        {
            throw new BadRequestException(nameof(dto.EmploymentDate), ErrorWhenDateEarlyToday);
        }

        if (dto.Category is Category.None)
        {
            throw new BadRequestException(nameof(dto.Category), ErrorWhenCategoryDontExist);
        }

        var contact = new Contact
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Category = dto.Category,
            EmploymentDate = dto.EmploymentDate,
        };

        if (dto.SkillIds?.Count > 0)
        {
            var skills = await _context.Skills.Where(x => dto.SkillIds.Contains(x.Id)).ToListAsync();
            contact.Skills = skills;
        }

        if (dto.SpecializationId is not null)
        {
            var specialization = await _context.Specializations.FirstOrDefaultAsync(x => x.Id == dto.SpecializationId);
            contact.Specialization = specialization;
        }

        await _context.AddAsync(contact);
        await _context.SaveChangesAsync();

        return contact.Id;
    }

    public async Task<ContactDto[]> SearchContactsAsync(string search)
    {
        if (string.IsNullOrEmpty(search) || search.Length < 3)
        {
            throw new BadRequestException(nameof(search), ErrorWhenSearchStringIsShort);
        }

        var comp = StringComparison.CurrentCultureIgnoreCase;
        var result = _context.Contacts
            .AsEnumerable() // в данном случае, когда база InMemory, нам необязательно делать все на стороне базы через например ef.functions.ilike()
            .Where(x => x.FirstName.Contains(search, comp) ||
                        x.LastName.Contains(search, comp) ||
                        x.Email.Contains(search, comp))
            .Select(x => x.ToContactDto())
            .ToArray();

        return result;
    }

    public async Task<ContactDto[]> GetContactsAsync()
    {
        return await _context.Contacts
            .Include(x => x.Specialization)
            .Include(x => x.Skills)
            .Select(x => x.ToContactDto())
            .ToArrayAsync();
    }
}