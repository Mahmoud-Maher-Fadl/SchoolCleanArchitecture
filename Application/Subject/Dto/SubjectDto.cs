using Mapster;
namespace Application.Subject.Dto;

public class SubjectDto:IRegister
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Hours { get; set; }
    public string InstructorName { get; set; }
    public string? DepartmentName { get; set; }
    public DateTime CreateDate { get; set; }
    public List<string>? StudentsNames { get; set; } = new List<string>();
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Model.Subject.Subject, SubjectDto>()
            .Map(dest => dest.InstructorName, src => src.Instructor.Name)
            .Map(dest => dest.DepartmentName, src => src.Department!.Name)
            .Map(dest=>dest.StudentsNames,src=>src.Students!.Select(student=>student.Name));
    }
}