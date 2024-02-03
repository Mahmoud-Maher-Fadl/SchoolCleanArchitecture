using Application.User.Dto;
using Domain.common;
using FluentValidation;
using MediatR;

namespace Application.User.Commands.UserPassword.ChangeUserPassword;
public class ChangeUserPasswordCommand:IRequest<Result<UserDto>>
{
    public string UserName { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public class Validator:AbstractValidator<ChangeUserPasswordCommand>
    {
        public Validator()
        {
            RuleFor(c => c.UserName).NotEmpty();
            RuleFor(c => c.OldPassword).NotEmpty();
            RuleFor(c => c.NewPassword).NotEmpty();
        }
    }
    /*
    public class Example : IMultipleExamplesProvider<ChangeUserPasswordCommand>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Example(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IEnumerable<SwaggerExample<ChangeUserPasswordCommand>> GetExamples()
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            var userName = applicationDbContext.Users.Select(x => x.UserName).FirstOrDefault() ?? string.Empty;
            var command=new ChangeUserPasswordCommand()
            {
                UserName = userName,
                OldPassword = "",
                NewPassword = "m@hmoud2019",
            };
            yield return SwaggerExample.Create("example", command);
        }
    }
    */

}