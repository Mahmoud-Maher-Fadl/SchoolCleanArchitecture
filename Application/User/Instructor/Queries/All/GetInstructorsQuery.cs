using Application.Instructor.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Instructor.Queries.All;

public class GetInstructorsQuery:IRequest<Result<List<InstructorDto>>>
{
}