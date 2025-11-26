using Microsoft.EntityFrameworkCore;
using SimpleContactForm.Abstractions.Dtos;
using SimpleContactForm.Abstractions.Interfaces;
using SimpleContactForm.DataAccess;

namespace SimpleContactForm.Service;

public class SpecializationService : ISpecializationService
{
    private readonly ContactFormDbContext _context;

    public SpecializationService(ContactFormDbContext context)
    {
        _context = context;
    }
    
    public async Task<ItemDto[]> GetSpecializationsAsync()
    {
        return await _context.Specializations.Select(x => new ItemDto(x.Id, x.Name, x.Code)).ToArrayAsync();
    }
}