using Domain.common;
using Domain.Tenant;

namespace Domain.Model.Student;

public class Student:BaseEntity
{
    public StudentStatus StudentStatus;
    public string? DepartmentId { get; set; }
    public Department.Department?  Department { get; set; }
    public HashSet<Subject.Subject> Subjects { get; set; } = new HashSet<Subject.Subject>();
}

public enum StudentStatus
{
    Student,
    Fired
}