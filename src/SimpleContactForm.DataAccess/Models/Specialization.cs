namespace SimpleContactForm.DataAccess.Models;

public class Specialization
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    
    public List<Contact>? Contacts { get; set; }

    public Specialization()
    {
    }

    public Specialization(string name, string code)
    {
        Name = name;
        Code = code;
    }
}