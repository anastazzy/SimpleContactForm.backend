using Microsoft.AspNetCore.Mvc;
using SimpleContactForm.Abstractions.Dtos;
using SimpleContactForm.Abstractions.Interfaces;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public Task<ItemDto[]> GetSkills()
    {
        return _skillService.GetSkillsAsync();
    }
}