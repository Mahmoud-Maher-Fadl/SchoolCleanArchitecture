using System.ComponentModel.DataAnnotations;
using Application.User.Dto;
using Domain.common;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Commands;

public class CreateUserCommand:IRequest<Result<UserDto>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    public class Validator:AbstractValidator<CreateUserCommand>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            // .Matches(x=>x.Password);
        }   
    }
    public class Handler:IRequestHandler<CreateUserCommand,Result<UserDto>>
    {
        private readonly UserManager<Domain.Identity.User> _userManager;
        public Handler(UserManager<Domain.Identity.User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var isExitEmail =await _userManager.FindByEmailAsync(request.Email);
            if (isExitEmail is not null)
                return Result.Failure<UserDto>($"This E-mail {request.Email} Is Already Used");
           
            var isExistUserName = await _userManager.FindByNameAsync(request.UserName);
            if (isExistUserName is not null)
                return Result.Failure<UserDto>($"This User Name {request.UserName} Is Already Used");
            
            var user = request.Adapt<Domain.Identity.User>();
            var password = _userManager.PasswordHasher.HashPassword(user,request.Password);
            user.PasswordHash = password;
            var result = await _userManager.CreateAsync(user);
            return result.Succeeded
                ? result.ToString().Adapt<UserDto>().AsSuccessResult()
                : Result.Failure<UserDto>(result.Errors.ToString() ?? string.Empty);
        }
    }
}