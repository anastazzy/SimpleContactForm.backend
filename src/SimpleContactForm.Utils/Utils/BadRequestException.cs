using System.Net;
using System.Text;

namespace SimpleContactForm.Utils.Utils;

public class BadRequestException : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;
    public IDictionary<string, string?[]> Errors { get; } = new Dictionary<string, string?[]>();
    
   

    public BadRequestException(string propertyName, string message)
    {
        Errors = new Dictionary<string, string?[]> { { propertyName, [message] } };
    }
    
    public BadRequestException(string message) : base(message)
    {
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(base.ToString());

        foreach (var error in Errors)
        {
            sb.Append($"{error.Key}: {string.Join(",", error.Value)}");
        }

        return base.ToString();
    }
}