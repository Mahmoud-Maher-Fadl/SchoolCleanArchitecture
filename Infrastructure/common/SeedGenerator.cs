using Microsoft.EntityFrameworkCore;

namespace Infrastructure.common;

public interface ISeedGenerator
{
    void Generate(ApplicationDbContext context);
}