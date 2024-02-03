/*
using Application.User.Student.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Student.Queries.Id;

public class Handler:IRequestHandler<GetStudentByIdQuery,Result<StudentDto>>
{
    private readonly IAdminContext _context;

    public Handler(IAdminContext context)
    {
        _context = context;
    }

    public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Tenants
            .Where(x => x.Id == request.Id)
            .Include(x=>x.Student)
            .ThenInclude(x=>x.Department)
            .Include(x=>x.Student)
            .ThenInclude(x=>x.Subjects).ProjectToType<StudentDto>()
            .FirstOrDefaultAsync(cancellationToken);
        return student is null
            ? Result.Failure<StudentDto>("Instructor not found")
            : student.AsSuccessResult();
    }
}
*/
