using Application.Student.Dto;
using Mapster;

namespace Application.Department.Dto;

public class DepartmentDto:IRegister
{
    public string Id { get; set; }
    public string Name { get; set; } 
    public string Location { get; set; }
    public List<string> StudentsNames { get; set; } = new List<string>();
    public List<string> SubjectsNames { get; set; } = new List<string>();
    //public List<StudentDto> Students { get; set; } = new List<StudentDto>();
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Model.Department.Department, DepartmentDto>()
          //  .Map(dest => dest.Students, src => src.Students.Adapt<List<StudentDto>>())
            .Map(dest=>dest.StudentsNames,src=>src.Students.Select(student=>student.Name))
            .Map(dest=>dest.SubjectsNames,src=>src.Subjects.Select(student=>student.Name));
    /*
    config.NewConfig<Domain.Model.Department.Department, DepartmentDto>()
            .Map(dest => dest.Students, src => src.Students != null ? src.Students.Adapt<List<StudentDto>>() : null);
    */
    }
}