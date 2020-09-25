using StellarPointer.WebApi.Models;
using StellarPointer.WebApi.Models.Coordinates;
using System.Text;

namespace StellarPointer.WebApi
{
    public class EphemerisApiUrlBuilder
    {
        private readonly StringBuilder apiUrlBuilder;

        public EphemerisApiUrlBuilder()
        {
            apiUrlBuilder = new StringBuilder();
            apiUrlBuilder.Append("http://ssp.imcce.fr/webservices/miriade/api/ephemcc.php?");
        }

        public EphemerisApiUrlBuilder AddName(StellarObjectDesignation stellarObjectDesignation)
        {
            apiUrlBuilder.Append($"-name={stellarObjectDesignation.Prefix}:{stellarObjectDesignation.Name}&");
            return this;
        }

        public EphemerisApiUrlBuilder AddEpoch(double epochInJulianDate)
        {
            apiUrlBuilder.Append($"-ep={epochInJulianDate}&");
            return this;
        }

        public EphemerisApiUrlBuilder AddJsonMime()
        {
            apiUrlBuilder.Append("-mime=json&");
            return this;
        }

        public EphemerisApiUrlBuilder AddObserver(Observer observer)
        {
            apiUrlBuilder.Append($"-observer={observer.Latitude},{observer.Longitude},{observer.Altitude}&");
            return this;
        }

        public EphemerisApiUrlBuilder AddCoordinateTypeOne()
        {
            apiUrlBuilder.Append("-tcoor=1&");
            return this;
        }

        public EphemerisApiUrlBuilder AddEphemerisTypeTwo()
        {
            apiUrlBuilder.Append("-teph=2");
            return this;
        }

        public string Build()
        {
            return apiUrlBuilder.ToString();
        }
    }
}
