using Domain.common;
using Domain.Model.Instructor;
using Infrastructure.common;

namespace Infrastructure.Instructor;

public class InstructorRepository:BaseSqlRepositoryImpl<Domain.Model.Instructor.Instructor>,IInstructorRepository
{
    public InstructorRepository(IApplicationDbContext context) : base(context.Instructors)
    {
    }
}