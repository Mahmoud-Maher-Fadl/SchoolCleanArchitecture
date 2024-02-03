using Domain.common;
using Infrastructure.common;
using Newtonsoft.Json;

namespace Infrastructure.Subject;

public class SubjectSeeds:ISeedGenerator
{
    public void Generate(IApplicationDbContext context)
    {
        if (context.Subjects.Any())
            return;
        var file = Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Subjects.json");
        var json = File.ReadAllText(file);
        var subjects = JsonConvert.DeserializeObject<List<Domain.Model.Subject.Subject>>(json);
        context.Subjects.AddRangeAsync(subjects);
    }
}