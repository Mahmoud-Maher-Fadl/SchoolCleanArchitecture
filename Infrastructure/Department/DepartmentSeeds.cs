using Infrastructure.common;
using Newtonsoft.Json;

namespace Infrastructure.Department;

public class DepartmentSeeds : ISeedGenerator
{
    public void Generate(ApplicationDbContext context)
    {
        if (context.Departments.Any())
            return;
        var file = Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Departments.json");
        var json = File.ReadAllText(file);
        var departments = JsonConvert.DeserializeObject<List<Domain.Model.Department.Department>>(json);
        context.Departments.AddRange(departments);
    }   
}