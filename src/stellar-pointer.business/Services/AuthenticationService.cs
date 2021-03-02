using StellarPointer.Persistence;
using System.Threading.Tasks;

namespace StellarPointer.Business.Services
{
    public class AuthenticationService
    {
        private readonly CredentialsService credentialsService;
        private readonly TokenService tokenService;

        public AuthenticationService(CredentialsService credentialsService, TokenService tokenService)
        {
            this.credentialsService = credentialsService;
            this.tokenService = tokenService;
        }

        public async Task<string> AuthenticateAsync(User user)
        {
            await credentialsService.ValidateCredentials(user);
            string securityToken = tokenService.GetToken();

            return securityToken;
        }
    }
}
