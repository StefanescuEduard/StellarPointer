using MediatR;
using StellarPointer.Business.Services;
using System.Threading;
using System.Threading.Tasks;

namespace StellarPointer.Business.Commands
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, string>
    {
        private readonly CredentialsService credentialsService;
        private readonly TokenService tokenService;

        public AuthenticateCommandHandler(CredentialsService credentialsService, TokenService tokenService)
        {
            this.credentialsService = credentialsService;
            this.tokenService = tokenService;
        }

        public async Task<string> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            await credentialsService.ValidateCredentials(request.User);
            string securityToken = tokenService.GetToken();

            return securityToken;
        }
    }
}
