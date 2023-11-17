using Application.User.Dto;
using Domain.common;
using Domain.Identity;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Commands;

public class ChangeUserPasswordCommand:IRequest<Result<UserDto>>
{
    public string UserName { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public class Validator:AbstractValidator<ChangeUserPasswordCommand>
    {
        public Validator()
        {
            RuleFor(c => c.UserName).NotEmpty();
            RuleFor(c => c.OldPassword).NotEmpty();
            RuleFor(c => c.NewPassword).NotEmpty();
        }
    }
    public class Handler:IRequestHandler<ChangeUserPasswordCommand,Result<UserDto>>
    {
        private readonly UserManager<SchoolUser> _userManager;
        private readonly IPasswordHasher<SchoolUser> _passwordHasher;

        public Handler(UserManager<SchoolUser> userManager, IPasswordHasher<SchoolUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<UserDto>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
                return Result.Failure<UserDto>("User With This User Name Not Found");
            
            //var oldHashed = _userManager.PasswordHasher.HashPassword(user, request.OldPassword);
            /*if(! await _userManager.CheckPasswordAsync(user, request.OldPassword) )
                return Result.Failure<UserDto>("Incorrect Old Password");*/
            var result=await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            /*
            user.PasswordHash= _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
            var result = await _userManager.UpdateAsync(user,request.NewPassword);
            */
            return result.Succeeded
                ? user.Adapt<UserDto>().AsSuccessResult()
                : Result.Failure<UserDto>(result.Errors.ToString() ?? string.Empty);


        }
    }
}