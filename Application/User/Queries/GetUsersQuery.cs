﻿using Application.User.Dto;
using Domain.common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Queries;

public class GetUsersQuery:IRequest<Result<List<UserDto>>>
{
    public class Handler:IRequestHandler<GetUsersQuery,Result<List<UserDto>>>
    {
        private readonly UserManager<Domain.Identity.SchoolUser> _userManager;

        public Handler(UserManager<Domain.Identity.SchoolUser> userManager)
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
}