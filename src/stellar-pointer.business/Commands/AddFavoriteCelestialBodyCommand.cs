using MediatR;

namespace StellarPointer.Business.Commands
{
    public class AddFavoriteCelestialBodyCommand : IRequest
    {
        public string Username { get; }
        public string CelestialBodyName { get; }

        public AddFavoriteCelestialBodyCommand(string username, string celestialBodyName)
        {
            Username = username;
            CelestialBodyName = celestialBodyName;
        }
    }
}