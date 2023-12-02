using Application.User.Dto;
using Domain.common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Queries.All;

public class GetUsersQuery:IRequest<Result<List<UserDto>>>
{
}