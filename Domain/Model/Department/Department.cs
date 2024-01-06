using Domain.common;
using Domain.Identity;

namespace Domain.Model.Department;

public class Department:BaseEntity
{
    public string Name { get; set; } 
    public string Location { get; set; } 
    public HashSet<User> Users { get; set; } = new HashSet<User>();
    public HashSet<Subject.Subject> Subjects { get; set; } = new HashSet<Subject.Subject>();
}