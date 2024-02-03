using Domain.common;
using Domain.Model.Subject;
using Infrastructure.common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Subject;

public class SubjectRepository:BaseSqlRepositoryImpl<Domain.Model.Subject.Subject>,ISubjectRepository
{
    public SubjectRepository(IApplicationDbContext context) : base(context.Subjects)
    {
    }
}