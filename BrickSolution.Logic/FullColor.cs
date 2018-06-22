using MonoBrickFirmware.Sensors;
using System;
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

                double actualRg = this.RGBColor.Red / this.RGBColor.Green;
                double expectedRg = expectedColor.RGBColor.Red / expectedColor.RGBColor.Green;
                double actualGb = this.RGBColor.Green / this.RGBColor.Blue;
                double expectedGb = expectedColor.RGBColor.Green / expectedColor.RGBColor.Blue;
                double actualBr = this.RGBColor.Blue / this.RGBColor.Red;
                double expectedBr = expectedColor.RGBColor.Blue / expectedColor.RGBColor.Red;

                double downFact = 1 - Constants.COLOUR_TOLERANCE;
                double upFact = 1 + Constants.COLOUR_TOLERANCE;

                bool colorMatch = expectedRg * downFact < actualRg
                    && actualRg < expectedRg * upFact
                    && expectedGb * downFact < actualGb
                    && actualGb < expectedGb * upFact
                    && expectedBr * downFact < actualBr
                    && actualBr < expectedBr;

                return colorMatch
                    //&& intensityMatch
                    ;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// returns a string representation of the object
        /// </summary>
        /// <returns>the string representation of this object</returns>
        public override string ToString()
        {
            return $"RGB:{System.Environment.NewLine}"
                 + $"  red:   {this.RGBColor.Red}{System.Environment.NewLine}"
                 + $"  blue:  {this.RGBColor.Blue}{System.Environment.NewLine}"
                 + $"  green: {this.RGBColor.Green}{System.Environment.NewLine}"
                 + $"{System.Environment.NewLine}"
                 + $"color-intensity: {this.Intensity}";
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
