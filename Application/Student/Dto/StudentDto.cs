using Mapster;

namespace Application.Student.Dto;

public class StudentDto:IRegister
{
    
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string? DepartmentName { get; set; }
    public List<string>? SubjectsNames { get; set; } = new List<string>();


    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Model.Student.Student, StudentDto>()
            .Map(dest => dest.DepartmentName, src => src.Department!.Name)
            .Map(dest=>dest.SubjectsNames,src=>src.Subjects!.Select(subject=>subject.Name));

    }
}