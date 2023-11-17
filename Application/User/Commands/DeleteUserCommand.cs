using Application.User.Dto;
using Domain.common;
using Domain.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Commands;

public class DeleteUserCommand:IRequest<Result<UserDto>>
{
    public string Id { get; set; } 
    public class Handler:IRequestHandler<DeleteUserCommand,Result<UserDto>>
    {
        private readonly UserManager<SchoolUser> _userManager;

        public Handler(UserManager<SchoolUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<UserDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user =await _userManager.FindByIdAsync(request.Id);
            if(user is null)
                return Result.Failure<UserDto>("User Doesn't Exist");
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Result.Success(user.Adapt<UserDto>());
            else
                return Result.Failure<UserDto>(result.Errors.FirstOrDefault()?.Description ?? "User Delete failed.");
        }
    }
}