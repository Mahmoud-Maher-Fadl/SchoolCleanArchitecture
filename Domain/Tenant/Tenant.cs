using Domain.Model.Department;
using Domain.Model.Instructor;
using Domain.Model.Student;
using Microsoft.AspNetCore.Identity;

namespace Domain.Tenant;
public class Tenant:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public Type Type { get; set; }
    public string SchoolId { get; set; } = string.Empty;
    public School School { get; set; }
}

public enum Type
{
    Student,
    Instructor
}