using SimpleContactForm.Abstractions.Dtos;

namespace SimpleContactForm.Abstractions.Interfaces;

public interface ISpecializationService
{
    Task<ItemDto[]> GetSpecializationsAsync();
}