using MediatR;
using StellarPointer.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace StellarPointer.Business.Commands
{
    public class AddFavoriteCelestialBodyCommandHandler : AsyncRequestHandler<AddFavoriteCelestialBodyCommand>
    {
        private readonly UserRepository userRepository;

        public AddFavoriteCelestialBodyCommandHandler(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        protected override Task Handle(AddFavoriteCelestialBodyCommand request, CancellationToken cancellationToken)
        {
            return userRepository.AddFavoriteStellarObjectAsync(request.Username, request.CelestialBodyName);
        }
    }
}