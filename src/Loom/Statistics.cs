#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Loom
{
    /// <summary>
    ///     Statistical functions
    /// </summary>
    public static class Statistics
    {
        /// <summary>
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static double CalculateMean(IEnumerable<double> values)
        {
            Argument.Assert.IsNotNull(values, nameof(values));

            return CalculateMeanPrivate(values);
        }

        /// <summary>
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static double CalculateStandardDeviation(IEnumerable<double> values)
        {
            Argument.Assert.IsNotNull(values, nameof(values));

            double mean;
            return CalculateStandardDeviationPrivate(values, out mean);
        }

        /// <summary>
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="mean">The mean.</param>
        /// <returns></returns>
        public static double CalculateStandardDeviation(IEnumerable<double> values, out double mean)
        {
            Argument.Assert.IsNotNull(values, nameof(values));

            return CalculateStandardDeviationPrivate(values, out mean);
        }

        private static double CalculateMeanPrivate(IEnumerable<double> values)
        {
            double sum = 0;
            int count = 0;

            foreach (double d in values)
            {
                sum += d;
                count++;
            }

            return sum / count;
        }

        private static double CalculateStandardDeviationPrivate(IEnumerable<double> values, out double mean)
        {
            IEnumerable<double> enumerable = values as double[] ?? values.ToArray();

            mean = CalculateMean(enumerable);

            double sumOfDiffSquares = 0;
            int count = 0;

            foreach (double d in enumerable)
            {
                double diff = d - mean;
                sumOfDiffSquares += diff * diff;
                count++;
            }

            return Math.Sqrt(sumOfDiffSquares / count);
        }
    }
}