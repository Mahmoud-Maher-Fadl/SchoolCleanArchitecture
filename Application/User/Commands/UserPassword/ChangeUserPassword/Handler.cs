using Application.User.Dto;
using Domain.common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Commands.UserPassword.ChangeUserPassword;
public class Handler:IRequestHandler<ChangeUserPasswordCommand,Result<UserDto>>
{
    private readonly UserManager<Domain.Tenant.Tenant> _userManager;
    private readonly IPasswordHasher<Domain.Tenant.Tenant> _passwordHasher;

    public Handler(UserManager<Domain.Tenant.Tenant> userManager, IPasswordHasher<Domain.Tenant.Tenant> passwordHasher)
    {
        _userManager = userManager;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserDto>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is null)
            return Result.Failure<UserDto>("Tenant With This Tenant Name Not Found");
        var result=await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        return result.Succeeded
            ? user.Adapt<UserDto>().AsSuccessResult()
            : Result.Failure<UserDto>(result.Errors.ToString() ?? string.Empty);
    }
}
