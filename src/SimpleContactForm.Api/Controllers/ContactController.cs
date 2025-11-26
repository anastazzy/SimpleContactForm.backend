using Microsoft.AspNetCore.Mvc;
using SimpleContactForm.Abstractions.Dtos;
using SimpleContactForm.Abstractions.Interfaces;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactService _service;

    public ContactController(IContactService service)
    {
        _service = service;
    }

    [HttpPost]
    public Task<Guid> AddAsync(CreateContactDto dto)
    {
       return _service.CreateContactAsync(dto);
    }
    
    [HttpGet("find")]
    public Task<ContactDto[]> SearchAsync([FromQuery] string search)
    {
        return _service.SearchContactsAsync(search);
    }
    
    [HttpGet]
    public Task<ContactDto[]> GetAsync()
    {
        return _service.GetContactsAsync();
    }
}