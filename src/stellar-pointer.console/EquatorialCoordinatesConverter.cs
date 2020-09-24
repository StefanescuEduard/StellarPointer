using System;

namespace StellarPointer.SerialWriter
{
    /*
     * Source: https://gist.github.com/matshofman/4145718
     */

    public class EquatorialCoordinatesConverter
    {
        /// <summary>
        /// DateTime will be set to current UTC time
        /// </summary>
        /// <param name="rightAscension">The right ascension in decimal value</param>
        /// <param name="declination">The declination in decimal value</param>
        /// <param name="earthLatitude">The latitude in decimal value</param>
        /// <param name="earthLongitude">The longitude in decimal value</param>
        /// <returns>The altitude and azimuth in decimal value</returns>
        public static CelestialCoordinates ToCelestialCoordinates(double rightAscension,
            double declination,
            double earthLatitude,
            double earthLongitude)
        {
            return ToCelestialCoordinates(rightAscension, declination, earthLatitude, earthLongitude, DateTime.UtcNow);
        }

        /// <summary>
        /// </summary>
        /// <param name="rightAscension">The right ascension in decimal value</param>
        /// <param name="declination">The declination in decimal value</param>
        /// <param name="earthLatitude">The latitude in decimal value</param>
        /// <param name="earthLongitude">The longitude in decimal value</param>
        /// <param name="date">The date(time) in UTC</param>
        /// <returns>The altitude and azimuth in decimal value</returns>
        public static CelestialCoordinates ToCelestialCoordinates(double rightAscension,
            double declination,
            double earthLatitude,
            double earthLongitude,
            DateTime date)
        {
            // Day offset and Local Siderial Time
            double dayOffset = (date - new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc)).TotalDays;
            double LST = (100.46 + 0.985647 * dayOffset + earthLongitude + 15 * (date.Hour + date.Minute / 60d) + 360) % 360;

            // Hour Angle
            double hourAngle = (LST - rightAscension + 360) % 360;

            // HA, DEC, Lat to Alt, AZ
            double x = Math.Cos(hourAngle * (Math.PI / 180)) * Math.Cos(declination * (Math.PI / 180));
            double y = Math.Sin(hourAngle * (Math.PI / 180)) * Math.Cos(declination * (Math.PI / 180));
            double z = Math.Sin(declination * (Math.PI / 180));

            double xhor = x * Math.Cos((90 - earthLatitude) * (Math.PI / 180)) - z * Math.Sin((90 - earthLatitude) * (Math.PI / 180));
            double yhor = y;
            double zhor = x * Math.Sin((90 - earthLatitude) * (Math.PI / 180)) + z * Math.Cos((90 - earthLatitude) * (Math.PI / 180));

            double az = Math.Atan2(yhor, xhor) * (180 / Math.PI) + 180;
            double alt = Math.Asin(zhor) * (180 / Math.PI);

            return new CelestialCoordinates { Altitude = alt, Azimuth = az };
        }
    }
}