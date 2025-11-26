using SimpleContactForm.Abstractions.Dtos;

namespace SimpleContactForm.Abstractions.Interfaces;

public interface ISkillService
{
    Task<ItemDto[]> GetSkillsAsync();
}