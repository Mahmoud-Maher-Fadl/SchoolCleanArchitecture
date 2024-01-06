using Domain.common;
using Domain.Identity;

namespace Domain.Model.Instructor;

public class Instructor:BaseEntity
{
    public Status Status { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public HashSet<Subject.Subject> Subjects { get; set; } = new HashSet<Subject.Subject>();
}

public enum Status
{
    Intern,
    Employed,
    Fired
}