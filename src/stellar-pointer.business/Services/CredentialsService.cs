using StellarPointer.Persistence;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace StellarPointer.Business.Services
{
    public class CredentialsService
    {
        private readonly UserRepository userRepository;

        public CredentialsService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task ValidateCredentials(User user)
        {
            User foundUser = await userRepository.GetAsync(user.Username);
            bool isValid = foundUser != null && foundUser.Password.Equals(user.Password);

            if (!isValid)
            {
                throw new InvalidCredentialException();
            }
        }
    }
}
