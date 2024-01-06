using Domain.common;
using Domain.Identity;

namespace Domain.Model.Student;

public class Student:BaseEntity
{
    public StudentStatus StudentStatus;
    public string? UserId { get; set; }
    public User? User { get; set; }
    public HashSet<string> SubjectsId = new HashSet<string>();
    public HashSet<Subject.Subject> Subjects { get; set; } = new HashSet<Subject.Subject>();
}

public enum StudentStatus
{
    Student,
    Fired
}