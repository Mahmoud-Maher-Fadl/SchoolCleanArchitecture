using Microsoft.AspNetCore.Identity;
namespace Domain.Identity;
public class SchoolUser:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
}