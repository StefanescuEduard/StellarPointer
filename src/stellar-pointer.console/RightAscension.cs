using System;

namespace StellarPointer.SerialWriter
{
    public class RightAscension
    {
        public double Longitude { get; private set; }

        public void ConvertFromArcSec(string arcSec)
        {
            arcSec = arcSec.TrimStart('+');
            string[] arcSecSeparatedForConversion = arcSec.Split(':');

            double hours = Convert.ToDouble(arcSecSeparatedForConversion[0]);
            double minutes = Convert.ToDouble(arcSecSeparatedForConversion[1]);
            double seconds = Convert.ToDouble(arcSecSeparatedForConversion[2]);

            const double conversionAngle = 15;
            Longitude = (hours +
                        minutes / TimeConstants.MinutesOfOneHour +
                        seconds / TimeConstants.SecondsOfOneHour) * conversionAngle;

            const double halfOfFullDegrees = 180;
            if (Longitude > halfOfFullDegrees)
            {
                const double fullCircleDegrees = 360;
                Longitude -= fullCircleDegrees;
            }
        }
    }
}