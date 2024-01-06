using Domain.Model.Department;
using Domain.Model.Instructor;
using Domain.Model.Student;
using Domain.Model.Subject;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity;
public class User:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public Type Type { get; set; }
    public string? DepartmentId { get; set; }
    public Department? Department { get; set; }
    public Student? Student { get; set; }
    public Instructor? Instructor { get; set; }
}

public enum Type
{
    Student,
    Instructor
}