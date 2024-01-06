using Domain.common;
using Domain.JWT;
using Domain.Role;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Role.Commands.Update;

public class UpdateRolCommand:IRequest<Result<RoleDto>>
{
    public string Id { get; set; }
    public string RoleName { get; set; }
    public class Validator : AbstractValidator<UpdateRolCommand>
    {
        private readonly IRoleRepo _roleRepo;
        public Validator(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
            ValidateName();
        }

        private  void ValidateName()
        {
            RuleFor(c => c.RoleName).NotEmpty()
                .MustAsync(async (key, cancellationToken) => !await _roleRepo.IsExistRule(key))
                .WithMessage("Role name must be unique.");

        }
    
    }
    
    public class Example : IMultipleExamplesProvider<UpdateRolCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<UpdateRolCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var role = applicationDbContext.Roles.FirstOrDefault();
            yield return SwaggerExample.Create("example",role?.Adapt<UpdateRolCommand>() ?? new UpdateRolCommand());
        }
    }


}