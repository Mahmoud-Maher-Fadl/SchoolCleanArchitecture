using System.ComponentModel.DataAnnotations;
using Application.User.Dto;
using Domain.common;
using Domain.Identity;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Commands;

public class UpdateUserCommand:IRequest<Result<UserDto>>
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
    
    public class Validator:AbstractValidator<UpdateUserCommand>
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
        }   
    }
    public class Handler:IRequestHandler<UpdateUserCommand,Result<UserDto>>
    {
        private readonly UserManager<Domain.Identity.User> _userManager;

        public Handler(UserManager<Domain.Identity.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if(user is null)
                return Result.Failure<UserDto>("User Doesn't Exist");
            
            var isExitEmail =await _userManager.FindByEmailAsync(request.Email);
            if (isExitEmail is not null)
                return Result.Failure<UserDto>($"This E-mail {request.Email} Is Already Used");
           
            var isExistUserName = await _userManager.FindByNameAsync(request.UserName);
            if (isExistUserName is not null)
                return Result.Failure<UserDto>($"This User Name {request.UserName} Is Already Used");

            request.Adapt(user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Result.Success(user.Adapt<UserDto>());
            }
            else
                return Result.Failure<UserDto>(result.Errors.FirstOrDefault()?.Description ?? "User Update failed.");
        }
    }
}