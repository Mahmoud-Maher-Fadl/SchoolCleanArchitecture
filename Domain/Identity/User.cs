using Domain.common;
using Microsoft.AspNetCore.Identity;
namespace Domain.Identity;
public class User:IdentityUser,Base
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}