using Mapster;
using Type = Domain.Tenant.Type;

namespace Application.Department.Dto;

public class DepartmentDto:IRegister
{
    public string Id { get; set; }
    public string Name { get; set; } 
    public string Location { get; set; }
    public DateTime CreateDate { get; set; }
    public List<string>? StudentsNames { get; set; } = new List<string>();
    public List<string>? SubjectsNames { get; set; } = new List<string>();
    public List<string>? InstructorsNames { get; set; } = new List<string>();
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Model.Department.Department, DepartmentDto>()
            // may cause a null reference exception
           // .Map(dest=>dest.StudentsNames,src=>src.Students.Select(x=>x.User.UserName))
           // .Map(dest=>dest.InstructorsNames,src=>src.Instructors.Select(x=>x.User.UserName))
            .Map(dest=>dest.SubjectsNames,src=>src.Subjects!.Select(subject=>subject.Name));
   
    }
}