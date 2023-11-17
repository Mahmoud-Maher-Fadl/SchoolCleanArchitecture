using Application.User.Dto;
using Domain.common;
using Domain.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Queries;

public class GetUserByIdQuery:IRequest<Result<UserDto>>
{
    public string Id { get; set; }
    public class Handler:IRequestHandler<GetUserByIdQuery,Result<UserDto>>
    {
        private readonly UserManager<SchoolUser> _userManager;

        public Handler(UserManager<SchoolUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            return user is not null
                ? user.Adapt<UserDto>().AsSuccessResult()
                : Result.Failure<UserDto>("User Not Found");
        }
    }
}