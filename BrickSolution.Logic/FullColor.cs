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
                double actualRg, actualGb, actualBr,
                    expectedRg, expectedGb, expectedBr;

                FullColor expectedColor = other as FullColor;

                this.GetRatios(out actualRg, out actualGb, out actualBr);
                expectedColor.GetRatios(out expectedRg, out expectedGb, out expectedBr);

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
        /// returns all relevant ratios
        /// </summary>
        /// <param name="rg">ratio between red and green</param>
        /// <param name="gb">ratio between green and blue</param>
        /// <param name="br">ratio blue and red</param>
        public void GetRatios(out double rg, out double gb, out double br)
        {
            rg = this.RGBColor.Red / (double)this.RGBColor.Green;
            gb = this.RGBColor.Green / (double)this.RGBColor.Blue;
            br = this.RGBColor.Blue / (double)this.RGBColor.Red;
        }

        /// <summary>
        /// returns a string representation of the object
        /// </summary>
        /// <returns>the string representation of this object</returns>
        public override string ToString()
        {
            return $"{this.RGBColor.Red}, "
                 + $"{this.RGBColor.Green}, "
                 + $"{this.RGBColor.Blue}, "
                 + $"{this.Intensity}";
        }

        public override int GetHashCode()
        {
            var hashCode = -112779971;
            hashCode = hashCode * -1521134295 + Intensity.GetHashCode();
            hashCode =
                hashCode * -1521134295
                + EqualityComparer<RGBColor>.Default.GetHashCode(RGBColor);
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
            return this.RGBColor.Red > 25
                || this.RGBColor.Green > 25
                || this.RGBColor.Blue > 25
                ||    (this.RGBColor.Red > 10
                    && this.RGBColor.Green > 10
                    && this.RGBColor.Blue > 10);
        }
    }
}
