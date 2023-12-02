using Domain.common;

namespace Domain.Model.Student;

public class Student:BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string? DepartmentId { get; set; }
    public Department.Department? Department { get; set; }

    public HashSet<Subject.Subject> Subjects { get; set; } = new HashSet<Subject.Subject>();
    /*public virtual Department.Department Department { get; set; }
    public virtual List<Subject.Subject> Subjects { get; set; }*/

    /* The virtual keyword on the navigation properties suggests that
       these properties can be overridden in derived classes,
       which is common in entity framework scenarios to enable lazy loading.*/
}