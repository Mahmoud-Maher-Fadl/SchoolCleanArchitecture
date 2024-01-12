using Application.User.Student.Dto;
using Domain.common;
using FluentValidation;
using MediatR;

namespace Application.User.Student.Commands.Update;

public class UpdateStudentCommand:IRequest<Result<StudentDto>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string? DepartmentId { get; set; }

    public List<string>? SubjectsId { get; set; } = new List<string>();

    public class Validator:AbstractValidator<UpdateStudentCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.Phone).NotEmpty();
        }
    }
    
}