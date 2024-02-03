using Domain.common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.common;

public interface ISeedGenerator
{
    void Generate(IApplicationDbContext context);
}