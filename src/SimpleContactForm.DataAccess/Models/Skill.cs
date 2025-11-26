namespace SimpleContactForm.DataAccess.Models;

public class Skill
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public List<Contact>? Contacts { get; set; }

    public Skill()
    {
    }

    public Skill(string name)
    {
        Name = name;
    }
}