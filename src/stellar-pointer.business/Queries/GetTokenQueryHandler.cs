using MediatR;
using StellarPointer.Business.Services;
using System.Threading;
using System.Threading.Tasks;

namespace StellarPointer.Business.Queries
{
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, string>
    {
        private readonly TokenService tokenService;

        public GetTokenQueryHandler(TokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public Task<string> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(tokenService.GetToken());
        }
    }
}