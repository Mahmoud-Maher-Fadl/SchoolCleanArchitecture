using Domain.common;
using Domain.JWT;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands;

public class SignInCommand:IRequest<Result<string>>
{
    public string UserName { get; set; }

    public string Password { get; set; }
    
    public class Validator:AbstractValidator<SignInCommand>
    {
        public Validator()
        {
            RuleFor(c => c.UserName).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
    
    public class Handler:IRequestHandler<SignInCommand, Result<string>>
    {
        private readonly UserManager<Domain.Identity.User> _userManager;
        private readonly SignInManager<Domain.Identity.User> _signInManager;
        private readonly IJwtRepo _jwtRepo;

        public Handler(UserManager<Domain.Identity.User> userManager
            , SignInManager<Domain.Identity.User> signInManager, IJwtRepo jwtRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtRepo = jwtRepo;
        }

        public async Task<Result<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x=>x.UserName == request.UserName,cancellationToken);
            if (user is null)
                return Result.Failure<string>("User Doesn't Exist");
          
            
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if(! signInResult.Succeeded)
                return Result.Failure<string>("Incorrect Password");
           
            // GenerateToken
            var token =await _jwtRepo.GenerateToken(user);
            return Result.Success(token);
        }
    }

}