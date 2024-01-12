using Domain.common;
using Domain.Role;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Role.Commands.Create;

public class CreateRoleCommand:IRequest<Result<RoleDto>>
{
    public string RoleName { get; set; }
    
    
    public class Validator : AbstractValidator<CreateRoleCommand>
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
    public class Example : IMultipleExamplesProvider<CreateRoleCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<CreateRoleCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            yield return SwaggerExample.Create("Example", new CreateRoleCommand()
            {
                RoleName = "Admin",
            });
            
        }
    }

}
