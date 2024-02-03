using Domain.common;
using Domain.JWT;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands;

public class SignInCommand : IRequest<Result<string>>
{
    public string UserName { get; set; }

    public string Password { get; set; }

    public class Validator : AbstractValidator<SignInCommand>
    {
        public Validator()
        {
            RuleFor(c => c.UserName).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<SignInCommand, Result<string>>
    {
        private readonly UserManager<Domain.Tenant.Tenant> _userManager;
        private readonly IJwtRepo _jwtRepo;

        public Handler(UserManager<Domain.Tenant.Tenant> userManager, IJwtRepo jwtRepo)
        {
            _userManager = userManager;
            _jwtRepo = jwtRepo;
        }

        public async Task<Result<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
            if (user is null)
                return Result.Failure<string>("Tenant Doesn't Exist");
            var signInResult = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!signInResult)
                return Result.Failure<string>("Incorrect Password");
            var token = await _jwtRepo.GenerateToken(user);
            return Result.Success(token);
        }
    }
}