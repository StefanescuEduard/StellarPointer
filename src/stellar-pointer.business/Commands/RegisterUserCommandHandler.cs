using MediatR;
using StellarPointer.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace StellarPointer.Business.Commands
{
    public class RegisterUserCommandHandler : AsyncRequestHandler<RegisterUserCommand>
    {
        private readonly UserRepository userRepository;

        public RegisterUserCommandHandler(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        protected override Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            request.User.Id = request.User.Username;
            return userRepository.AddAsync(request.User);
        }
    }
}