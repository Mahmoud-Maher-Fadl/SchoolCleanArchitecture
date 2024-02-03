using System.ComponentModel.DataAnnotations;
using Application.User.Student.Dto;
using Domain.common;
using Domain.Model.Student;
using FluentValidation;
using MediatR;

namespace Application.User.Student.Commands.Update;

public class UpdateStudentCommand:IRequest<Result<StudentDto>>
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    public StudentStatus Status { get; set; }
    public string? DepartmentId { get; set; }
    public List<string>? SubjectsId { get; set; }
    public class Validator:AbstractValidator<UpdateStudentCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(c => c.Status).IsInEnum();
        }
    }
    
}