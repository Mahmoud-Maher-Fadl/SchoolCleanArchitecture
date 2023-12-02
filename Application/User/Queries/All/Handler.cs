using Application.User.Dto;
using Domain.common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Queries.All;

public class Handler:IRequestHandler<GetUsersQuery,Result<List<UserDto>>>
{
    private readonly UserManager<Domain.Identity.User> _userManager;

    public Handler(UserManager<Domain.Identity.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users
            .ProjectToType<UserDto>()
            .ToListAsync(cancellationToken);
        return users.AsSuccessResult();
    }
}
