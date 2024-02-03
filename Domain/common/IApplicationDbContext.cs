using Domain.Model.Department;
using Domain.Model.Instructor;
using Domain.Model.Student;
using Domain.Model.Subject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.common;

public interface IApplicationDbContext
{
    public DbSet<Department>Departments { get; set; }
    public DbSet<Student>Students { get; set; }
    public DbSet<Subject>Subjects { get; set; }
    public DbSet<Instructor>Instructors { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}