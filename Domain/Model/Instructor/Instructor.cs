using Domain.common;
using Domain.Tenant;

namespace Domain.Model.Instructor;

public class Instructor : BaseEntity
{
    public Status Status { get; set; }
    public HashSet<Subject.Subject> Subjects { get; set; } = new HashSet<Subject.Subject>();
    public string? DepartmentId { get; set; }
    public Department.Department? Department { get; set; }
}

public enum Status
{
    Intern,
    Employed,
    Fired
}