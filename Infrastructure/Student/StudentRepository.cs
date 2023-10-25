using Domain.Model.Student;
using Infrastructure.common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Student;

public class StudentRepository:BaseSqlRepositoryImpl<Domain.Model.Student.Student>,IStudentRepository
{
    public StudentRepository(ApplicationDbContext context) : base(context.Students)
    {
    }
}