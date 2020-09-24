using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StellarPointer.WebApi.Models.Coordinates
{
    public class PlanetCoordinates
    {
        [JsonPropertyName("RA")]
        public string RightAscensionInArcSec { get; set; }

        [JsonPropertyName("DEC")]
        public string DeclinationInArcSec { get; set; }
    }

    public class PlanetCoordinatesBodyResponse
    {
        [JsonPropertyName("data")]
        public IEnumerable<PlanetCoordinates> PlanetCoordinates { get; set; }
    }
}
