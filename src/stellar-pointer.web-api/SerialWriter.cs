using StellarPointer.WebApi.Models;
using StellarPointer.WebApi.Models.Coordinates;
using System;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StellarPointer.WebApi
{
    public class SerialWriter
    {
        private readonly EphemerisApiUrlBuilder ephemerisApiUrlBuilder;

        public SerialWriter()
        {
            ephemerisApiUrlBuilder = new EphemerisApiUrlBuilder();
        }

        public async Task PointSkyObject(StellarObjectDesignation stellarObjectDesignation)
        {
            var observer = new Observer
            {
                Latitude = 45.42546991419586,
                Longitude = 23.9497447013855,
                Altitude = 595
            };

            double epoch = GetJulianDate(DateTime.Now);
            PlanetCoordinates planetCoordinates = await GetPlanetCoordinates(stellarObjectDesignation, epoch, observer);

            var rightAscension = new RightAscension();
            rightAscension.ConvertFromArcSec(planetCoordinates.RightAscensionInArcSec);

            var declination = new Declination();
            declination.ConvertFromArcSec(planetCoordinates.DeclinationInArcSec);

            CelestialCoordinates celestialCoordinates = EquatorialCoordinatesConverter.ToCelestialCoordinates(
                rightAscension.Longitude,
                declination.Latitude,
                observer.Latitude,
                observer.Longitude);

            double roundedAltitude = Math.Round(celestialCoordinates.Altitude);
            double roundedAzimuth = Math.Round(celestialCoordinates.Azimuth);

            double servoMotorValue = roundedAltitude;

            if (servoMotorValue > 0)
            {
                servoMotorValue += 90;
            }
            else
            {
                servoMotorValue = 90 - Math.Abs(servoMotorValue);
            }

            using var serialPort = new SerialPort
            {
                PortName = "COM6",
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None,
                RtsEnable = true
            };

            serialPort.Open();

            var command = $"<S{roundedAzimuth};A{servoMotorValue};B{servoMotorValue};C{servoMotorValue}>";

            serialPort.WriteLine(command);
        }

        private async Task<PlanetCoordinates> GetPlanetCoordinates(StellarObjectDesignation stellarObjectDesignation,
            double epochInJulianDate,
            Observer observer)
        {
            using var httpClient = new HttpClient();
            string apiUrl = ephemerisApiUrlBuilder
                .AddName(stellarObjectDesignation)
                .AddEpoch(epochInJulianDate)
                .AddJsonMime()
                .AddObserver(observer)
                .AddCoordinateTypeOne()
                .AddEphemerisTypeTwo()
                .Build();

            HttpResponseMessage planetCoordinatesResponse = await httpClient.GetAsync(apiUrl);
            planetCoordinatesResponse.EnsureSuccessStatusCode();
            string planetCoordinatesResponseBody = await planetCoordinatesResponse.Content.ReadAsStringAsync();

            var planetCoordinatesBody =
                JsonSerializer.Deserialize<PlanetCoordinatesBodyResponse>(planetCoordinatesResponseBody);

            return planetCoordinatesBody.PlanetCoordinates.First();
        }

        private static double GetJulianDate(DateTime date)
        {
            const double julianDateOffset = 2415018.5;
            return date.ToOADate() + julianDateOffset;
        }
    }
}
