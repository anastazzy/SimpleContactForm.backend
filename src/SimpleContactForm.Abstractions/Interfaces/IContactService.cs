using SimpleContactForm.Abstractions.Dtos;

namespace SimpleContactForm.Abstractions.Interfaces;

public interface IContactService
{
    Task<Guid> CreateContactAsync(CreateContactDto dto);
    Task<ContactDto[]> SearchContactsAsync(string search);
    Task<ContactDto[]> GetContactsAsync();
}