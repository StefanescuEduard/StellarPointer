using System;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StellarPointer.SerialWriter
{
    public class Program
    {
        public static async Task Main()
        {
            var observer = new Observer
            {
                // long, lat, alt
            };

            double epoch = GetJulianDate(DateTime.Now);
            const string planetName = "Jupiter";
            PlanetCoordinates planetCoordinates = await GetPlanetCoordinates(planetName, epoch, observer);

            var rightAscension = new RightAscension();
            rightAscension.ConvertFromArcSec(planetCoordinates.RightAscensionInArcSec);

            var declination = new Declination();
            declination.ConvertFromArcSec(planetCoordinates.DeclinationInArcSec);

            Console.WriteLine($"Right ascension: {planetCoordinates.RightAscensionInArcSec}");
            Console.WriteLine($"Declination: {planetCoordinates.DeclinationInArcSec}");

            CelestialCoordinates celestialCoordinates = EquatorialCoordinatesConverter.ToCelestialCoordinates(
                rightAscension.Longitude,
                declination.Latitude,
                observer.Latitude,
                observer.Longitude);

            double roundedAltitude = Math.Round(celestialCoordinates.Altitude);
            double roundedAzimuth = Math.Round(celestialCoordinates.Azimuth);
            Console.WriteLine($"Round azimuth: {roundedAzimuth}");
            Console.WriteLine($"Round altitude: {roundedAltitude}");

            double servoMotorValue = roundedAltitude;

            if (servoMotorValue > 0)
            {
                servoMotorValue += 90;
            }
            else
            {
                servoMotorValue = 90 - Math.Abs(servoMotorValue);
            }

            using (var serialPort = new SerialPort())
            {
                serialPort.PortName = "COM6";
                serialPort.BaudRate = 9600;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                serialPort.RtsEnable = true;

                serialPort.Open();

                string command = $"<S{roundedAzimuth};A{servoMotorValue};B{servoMotorValue};C{servoMotorValue}>";

                serialPort.WriteLine(command);
            }

            Console.ReadKey();
        }

        public static async Task<PlanetCoordinates> GetPlanetCoordinates(string planetName, double epoch, Observer observer)
        {
            using (var httpClient = new HttpClient())
            {
                string apiUrl =
                    $"https://ssp.imcce.fr/webservices/miriade/api/ephemcc.php?-name=p:{planetName}&-ep={epoch}&-mime=json&-observer={observer.Latitude},{observer.Longitude},{observer.Altitude}&-tcoor=1&-teph=2";
                HttpResponseMessage planetCoordinatesResponse = await httpClient.GetAsync(apiUrl);
                planetCoordinatesResponse.EnsureSuccessStatusCode();
                string planetCoordinatesResponseBody = await planetCoordinatesResponse.Content.ReadAsStringAsync();

                var planetCoordinatesBody =
                    JsonSerializer.Deserialize<PlanetCoordinatesBodyResponse>(planetCoordinatesResponseBody);

                return planetCoordinatesBody.PlanetCoordinates.First();
            }
        }

        public static double GetJulianDate(DateTime date)
        {
            const double julianDateOffset = 2415018.5;
            return date.ToOADate() + julianDateOffset;
        }
    }
}