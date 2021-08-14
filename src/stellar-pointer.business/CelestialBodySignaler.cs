using System;
using System.Collections.Generic;
using System.Threading;

namespace StellarPointer.Business
{
    public class CelestialBodySignaler : IDisposable
    {
        private readonly Timer timer;

        public IEnumerable<SubscribedUser> SubscribedUsers { get; private set; }

        public CelestialBodySignaler()
        {
            var autoResetEvent = new AutoResetEvent(false);
            timer = new Timer(GetVisibleCelestialBodies, autoResetEvent, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        }

        private void GetVisibleCelestialBodies(object state)
        {
            SubscribedUsers = new List<SubscribedUser>
            {
                new SubscribedUser
                {
                    Username = "Eduard",
                    VisibleCelestialBodies = new List<string>
                    {
                        "Mars",
                        "Pluto"
                    }
                }
            };
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }

    public class SubscribedUser
    {
        public string Username { get; set; }

        public IEnumerable<string> VisibleCelestialBodies { get; set; }
    }
}
