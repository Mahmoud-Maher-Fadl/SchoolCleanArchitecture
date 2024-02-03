using Domain.common;
namespace Domain.Model.Department;

public class Department:BaseEntity
{
    public string Name { get; set; } 
    public string Location { get; set; } 
    public HashSet<Student.Student> Students { get; set; } = new HashSet<Student.Student>();
    public HashSet<Instructor.Instructor> Instructors { get; set; } = new HashSet<Instructor.Instructor>();
    public HashSet<Subject.Subject> Subjects { get; set; } = new HashSet<Subject.Subject>();
}