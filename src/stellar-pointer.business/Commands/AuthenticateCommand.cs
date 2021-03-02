using MediatR;
using StellarPointer.Persistence;

namespace StellarPointer.Business.Commands
{
    public class AuthenticateCommand : IRequest<string>
    {
        public User User { get; }

        public AuthenticateCommand(User user)
        {
            User = user;
        }
    }
}
