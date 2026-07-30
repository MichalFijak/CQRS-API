using Application.Common;
using Application.Dtos;
using Application.Services;
using Domain.Interfaces;
using MediatR;


namespace Application.Commands.User
{
    public sealed record LoginUserCommand(string Username, string Password)
        : IRequest<Result<LoginResponse>>
    {
    }

    internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginResponse>>
    {
        private readonly IUserRepository repo;
        private readonly IPasswordHasher hasher;
        private readonly ITokenService tokens;

        public LoginUserCommandHandler(IUserRepository repo, IPasswordHasher hasher, ITokenService tokens)
        {
            this.repo = repo;
            this.hasher = hasher;
            this.tokens = tokens;
        }

        public async Task<Result<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user =await repo.GetByUsernameAsync(request.Username);

            if (user is null) return Result<LoginResponse>.Fail("Invalid Credential");
            if (!hasher.Verify(request.Password, user.PasswordHash)) return Result<LoginResponse>.Fail("Invalid Credential");

            var accessToken = tokens.GenerateAccessToken(user);

            var (refreshToken, expiry) = tokens.GenerateRefreshToken();
            user.SetRefreshToken(refreshToken, expiry);

            await repo.UpdateAsync(user);

            return Result<LoginResponse>.Success(new LoginResponse(accessToken, refreshToken));

        }
    }
}
