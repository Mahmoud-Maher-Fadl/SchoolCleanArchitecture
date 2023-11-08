using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext:IdentityDbContext<Domain.Identity.SchoolUser>
{
   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    public DbSet<Domain.Model.Department.Department>Departments { get; set; }
    public DbSet<Domain.Model.Student.Student>Students { get; set; }
    public DbSet<Domain.Model.Subject.Subject>Subjects { get; set; }
    public DbSet<Domain.Identity.SchoolUser>SchoolUsers { get; set; }
}