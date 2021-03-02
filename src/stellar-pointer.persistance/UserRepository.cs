using System.Threading.Tasks;

namespace StellarPointer.Persistence
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(StellarPointerContext stellarPointerContext) : base(stellarPointerContext)
        {
        }

        public async Task AddFavoriteStellarObjectAsync(string username, string stellarObjectName)
        {
            User user = await GetAsync(username);
            user.FavoriteCelestialBodies.Add(stellarObjectName);

            await AddAsync(user);
        }
    }
}