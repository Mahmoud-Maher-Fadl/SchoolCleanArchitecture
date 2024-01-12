using Application.User.Instructor.Dto;
using Domain.common;
using MediatR;

namespace Application.User.Instructor.Queries.All;

public class GetInstructorsQuery:IRequest<Result<List<InstructorDto>>>
{
}