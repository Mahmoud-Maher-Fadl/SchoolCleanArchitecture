using Application.User.Dto;
using Domain.common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Commands.Create;

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
        var result = await _userManager.CreateAsync(user,request.Password);
        if (result.Succeeded) 
            return Result.Success(user.Adapt<UserDto>());
           
        else
        {
            // Return failure result with error messages
            return Result.Failure<UserDto>(result.Errors.FirstOrDefault()?.Description ?? "User creation failed.");
        }

    }
}
