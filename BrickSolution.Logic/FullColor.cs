using MonoBrickFirmware.Sensors;
using System.Collections.Generic;

namespace BrickSolution.Logic
{
    public class FullColor
    {
        /// <summary>
        /// describing light intensity/grey levels
        /// </summary>
        public int Intensity { get; set; }

        /// <summary>
        /// describing current rgb values
        /// </summary>
        public RGBColor RGBColor { get; set; }

        /// <summary>
        /// matches to <see cref="FullColor"/> object based on their color
        /// intensities and rgb values
        /// </summary>
        /// <param name="other">
        /// expects a other <see cref="FullColor"/> object to compare</param>
        /// <returns>
        /// true:  match succeeded
        /// false: match failed
        /// </returns>
        public override bool Equals(object other)
        {
            if (other is FullColor)
            {
                FullColor expectedColor = other as FullColor;

                bool colourMatch = expectedColor.RGBColor.Red - (Constants.COLOUR_TOLERANCE / 2) < this.RGBColor.Red
                    && this.RGBColor.Red < expectedColor.RGBColor.Red + (Constants.COLOUR_TOLERANCE / 2)
                    && expectedColor.RGBColor.Green - (Constants.COLOUR_TOLERANCE / 2) < this.RGBColor.Green
                    && this.RGBColor.Green < expectedColor.RGBColor.Green + (Constants.COLOUR_TOLERANCE / 2)
                    && expectedColor.RGBColor.Blue - (Constants.COLOUR_TOLERANCE / 2) < this.RGBColor.Blue
                    && this.RGBColor.Blue < expectedColor.RGBColor.Blue + (Constants.COLOUR_TOLERANCE / 2);

                // to implement
                bool intensityMatch = true;

                return colourMatch && intensityMatch;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -112779971;
            hashCode = hashCode * -1521134295 + Intensity.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<RGBColor>.Default.GetHashCode(RGBColor);
            return hashCode;
        }
    }
}
