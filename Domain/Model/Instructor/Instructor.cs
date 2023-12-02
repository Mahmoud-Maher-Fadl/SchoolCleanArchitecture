using Domain.common;

namespace Domain.Model.Instructor;

public class Instructor:BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public Status Status { get; set; }
    public string? DepartmentId { get; set; }
    public Department.Department? Department { get; set; }
    public HashSet<Subject.Subject> Subjects { get; set; } = new HashSet<Subject.Subject>();
}

public enum Status
{
    Intern,
    Employed,
    Fired
}