using Application.Common;
using Application.Services;
using Domain.Interfaces;


namespace Application.Commands.User
{
    public class RegisterUserCommandHandler
    {

        private readonly IUserRepository _repo;
        private readonly IPasswordHasher _hasher;

        public RegisterUserCommandHandler(IUserRepository repo, IPasswordHasher hasher)
        {
            _repo = repo;
            _hasher = hasher;
        }

        public async Task<Result> Handle(RegisterUserCommand cmd)
        {
            var existing = await _repo.GetByUsernameAsync(cmd.Username);
            if (existing != null)
                return Result.Fail("User exists");

            var hash = _hasher.Hash(cmd.Password);

            var user = new Domain.Entities.User(cmd.Username, hash);
            await _repo.AddAsync(user);

            return Result.Success();
        }
    }

    
}
