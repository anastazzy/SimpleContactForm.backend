using Microsoft.EntityFrameworkCore;
using SimpleContactForm.Abstractions.Dtos;
using SimpleContactForm.Abstractions.Interfaces;
using SimpleContactForm.DataAccess;

namespace SimpleContactForm.Service;

public class SkillService : ISkillService
{
    private readonly ContactFormDbContext _context;

    public SkillService(ContactFormDbContext context)
    {
        _context = context;
    }

    public async Task<ItemDto[]> GetSkillsAsync()
    {
        return await _context.Skills.Select(x => new ItemDto(x.Id, x.Name, null)).ToArrayAsync();
    }
}