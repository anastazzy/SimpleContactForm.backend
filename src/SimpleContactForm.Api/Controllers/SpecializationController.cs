using Microsoft.AspNetCore.Mvc;
using SimpleContactForm.Abstractions.Dtos;
using SimpleContactForm.Abstractions.Interfaces;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecializationController : ControllerBase
{
    private readonly ISpecializationService _skillService;

    public SpecializationController(ISpecializationService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public Task<ItemDto[]> GetSkills()
    {
        return _skillService.GetSpecializationsAsync();
    }
}