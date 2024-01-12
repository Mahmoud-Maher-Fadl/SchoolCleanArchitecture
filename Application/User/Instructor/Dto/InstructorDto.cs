using Domain.Model.Instructor;
using Mapster;

namespace Application.User.Instructor.Dto;

public class InstructorDto:IRegister
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Status Status { get; set; }
    public DateTime CreateDate { get; set; }
    public string? DepartmentName { get; set; }
    public List<string>? SubjectsNames { get; set; } = new List<string>();
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Identity.User, InstructorDto>()
            .Map(dest=>dest.UserId,src=>src.Id)
            .Map(dest => dest.DepartmentName, src => src.Department!=null?src.Department.Name:"")
            .Map(dest=>dest.SubjectsNames,src=>src.Instructor!=null?src.Instructor.Subjects.Select(subject=>subject.Name):Array.Empty<string>());
    }
}