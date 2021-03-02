using MediatR;
using StellarPointer.Persistence;

namespace StellarPointer.Business.Commands
{
    public class RegisterUserCommand : IRequest
    {
        public User User { get; }

        public RegisterUserCommand(User user)
        {
            User = user;
        }
    }
}
