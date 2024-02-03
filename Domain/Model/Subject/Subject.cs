using Domain.common;

namespace Domain.Model.Subject;

public class Subject:BaseEntity
{
    public string Name { get; set; }
    public int Hours { get; set; }
    public string? InstructorId { get; set; }
    public Instructor.Instructor? Instructor { get; set; }
    
    public string? DepartmentId { get; set; }
    public Department.Department? Department { get; set; }
    public HashSet<Student.Student> Students { get; set; } = new HashSet<Student.Student>();
}