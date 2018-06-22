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
                // e: 8, 34, 9, 37
                // a: 8, 35, 9, 37

                FullColor expectedColor = other as FullColor;

                double actualRg = this.RGBColor.Red / (double)this.RGBColor.Green;
                double expectedRg = expectedColor.RGBColor.Red / (double)expectedColor.RGBColor.Green;
                double actualGb = this.RGBColor.Green / (double)this.RGBColor.Blue;
                double expectedGb = expectedColor.RGBColor.Green / (double)expectedColor.RGBColor.Blue;
                double actualBr = this.RGBColor.Blue / (double)this.RGBColor.Red;
                double expectedBr = expectedColor.RGBColor.Blue / (double)expectedColor.RGBColor.Red;

                double downFact = 1 - Constants.COLOUR_TOLERANCE;
                double upFact = 1 + Constants.COLOUR_TOLERANCE;

                bool colorMatch = expectedRg * downFact < actualRg
                    && actualRg < expectedRg * upFact
                    && expectedGb * downFact < actualGb
                    && actualGb < expectedGb * upFact
                    && expectedBr * downFact < actualBr
                    && actualBr < expectedBr * upFact;

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
            return $"{this.RGBColor.Red}, {this.RGBColor.Green}, {this.RGBColor.Blue}, {this.Intensity}";
        }

        public override int GetHashCode()
        {
            var hashCode = -112779971;
            hashCode = hashCode * -1521134295 + Intensity.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<RGBColor>.Default.GetHashCode(RGBColor);
            return hashCode;
        }

        /// <summary>
        /// returns a boolean indicating if the measured values are
        /// significant or not
        /// </summary>
        /// <returns>
        /// true:  the fullColor is significant
        /// false: the fullcolor is not significant</returns>
        public bool IsSignificant()
        {
            // all over 10 or 1 over 25
            //to implement
            throw new NotImplementedException();
        }
    }
}
