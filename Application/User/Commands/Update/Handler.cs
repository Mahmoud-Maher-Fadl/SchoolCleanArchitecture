using Application.User.Dto;
using Domain.common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Commands.Update;

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
