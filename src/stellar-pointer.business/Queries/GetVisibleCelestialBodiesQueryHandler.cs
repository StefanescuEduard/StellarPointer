using MediatR;
using Microsoft.AspNetCore.SignalR;
using StellarPointer.Business.Hubs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StellarPointer.Business.Queries
{
    public class GetVisibleCelestialBodiesQueryHandler : AsyncRequestHandler<GetVisibleCelestialBodiesQuery>
    {
        private readonly IHubContext<CelestialBodyHub> hubContext;
        private readonly CelestialBodySignaler celestialBodySignaler;

        public GetVisibleCelestialBodiesQueryHandler(IHubContext<CelestialBodyHub> hubContext, CelestialBodySignaler celestialBodySignaler)
        {
            this.hubContext = hubContext;
            this.celestialBodySignaler = celestialBodySignaler;
        }

        protected override Task Handle(GetVisibleCelestialBodiesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<SubscribedUser> subscribedUsers = celestialBodySignaler.SubscribedUsers;
            foreach (SubscribedUser subscribedUser in subscribedUsers)
            {
                hubContext
                    .Clients
                    .Users(subscribedUser.Username)
                    .SendAsync(
                        "get-visible-celestial-bodies",
                        celestialBodySignaler.SubscribedUsers,
                        cancellationToken);
            }

            return hubContext.Clients.All.SendAsync("get-visible-celestial-bodies", celestialBodySignaler.SubscribedUsers, cancellationToken);
        }
    }
}