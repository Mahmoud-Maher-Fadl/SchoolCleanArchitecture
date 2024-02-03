using Domain.common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public class ApplicationDbContext:DbContext,IApplicationDbContext
{
   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<Domain.Model.Department.Department> Departments { get; set; }
    public DbSet<Domain.Model.Student.Student> Students { get; set; }
    public DbSet<Domain.Model.Subject.Subject> Subjects { get; set; }
    public DbSet<Domain.Model.Instructor.Instructor> Instructors { get; set; }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }
}