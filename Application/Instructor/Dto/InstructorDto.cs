using Application.Student.Dto;
using Domain.Model.Instructor;
using Mapster;

namespace Application.Instructor.Dto;

public class InstructorDto:IRegister
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public Status Status { get; set; }
    public DateTime CreateDate { get; set; }
    public string? DepartmentName { get; set; }
    public List<string>? SubjectsNames { get; set; } = new List<string>();
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Model.Instructor.Instructor, InstructorDto>()
            .Map(dest => dest.DepartmentName, src => src.Department!.Name)
            .Map(dest=>dest.SubjectsNames,src=>src.Subjects!.Select(subject=>subject.Name));
    }
}