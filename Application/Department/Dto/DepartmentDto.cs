using Mapster;
using Type = Domain.Identity.Type;

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
            .Map(dest=>dest.StudentsNames,src=>src.Users.Where(x=>x.Type==Type.Student).Select(x=>x.UserName))
            .Map(dest=>dest.InstructorsNames,src=>src.Users.Where(x=>x.Type==Type.Instructor).Select(x=>x.UserName))
            .Map(dest=>dest.SubjectsNames,src=>src.Subjects!.Select(subject=>subject.Name));
   
    }
}