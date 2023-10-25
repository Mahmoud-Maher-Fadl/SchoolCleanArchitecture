using Microsoft.EntityFrameworkCore;

namespace Infrastructure.common;

public interface SeedGenerator
{
    void Generate(ApplicationDbContext context);
}