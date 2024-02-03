using Domain.common;
using Domain.Tenant;
using Infrastructure.common;
using Microsoft.EntityFrameworkCore;

namespace School.School;

public class SchoolRepository:BaseSqlRepositoryImpl<Domain.Tenant.School>,ISchoolRepository
{
    public SchoolRepository(IAdminContext adminContext) : base(adminContext.Schools)
    {
    }
}