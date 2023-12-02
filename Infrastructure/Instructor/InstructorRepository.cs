using Domain.Model.Instructor;
using Infrastructure.common;

namespace Infrastructure.Instructor;

public class InstructorRepository:BaseSqlRepositoryImpl<Domain.Model.Instructor.Instructor>,IInstructorRepository
{
    public InstructorRepository(ApplicationDbContext context) : base(context.Instructors)
    {
    }
}