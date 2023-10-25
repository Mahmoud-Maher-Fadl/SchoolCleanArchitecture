using Domain.Model.Subject;
using Infrastructure.common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Subject;

public class SubjectRepository:BaseSqlRepositoryImpl<Domain.Model.Subject.Subject>,ISubjectRepository
{
    public SubjectRepository(ApplicationDbContext context) : base(context.Subjects)
    {
    }
}