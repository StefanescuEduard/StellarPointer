using System;

namespace StellarPointer.SerialWriter
{
    public class Declination
    {
        public double Latitude { get; private set; }

        public void ConvertFromArcSec(string arcSec)
        {
            arcSec = arcSec.TrimStart('+');
            string[] arcSecSeparatedForConversion = arcSec.Split(':');

            double degrees = Convert.ToDouble(arcSecSeparatedForConversion[0]);
            double hours = Convert.ToDouble(arcSecSeparatedForConversion[1]);
            double minutes = Convert.ToDouble(arcSecSeparatedForConversion[2]);

            Latitude = degrees + hours / TimeConstants.MinutesOfOneHour + minutes / TimeConstants.SecondsOfOneHour;
        }
    }
}