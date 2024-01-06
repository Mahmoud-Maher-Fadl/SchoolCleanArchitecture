using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext:IdentityDbContext<Domain.Identity.User>
{
   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    public DbSet<Domain.Model.Department.Department>Departments { get; set; }
    public DbSet<Domain.Model.Student.Student>Students { get; set; }
    public DbSet<Domain.Model.Subject.Subject>Subjects { get; set; }
    public DbSet<Domain.Identity.User>Users { get; set; }
    public DbSet<Domain.Model.Instructor.Instructor>Instructors { get; set; }
    public DbSet<Domain.Role.Role>Roles { get; set; }
}