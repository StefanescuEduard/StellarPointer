using CouchDB.Driver.Types;
using System.Collections.Generic;

namespace StellarPointer.Persistence
{
    public class User : CouchDocument
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public HashSet<string> FavoriteCelestialBodies { get; set; }

        public User()
        {
            FavoriteCelestialBodies = new HashSet<string>();
        }
    }
}