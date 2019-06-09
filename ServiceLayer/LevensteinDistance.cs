// *********************************************************************************
// <copyright file="LevensteinDistance.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************

namespace ServiceLayer
{
    using System;

    /// <summary>
    /// Class LevensteinDistance.cs
    /// </summary>
    public static class LevensteinDistance
    {
        /// <summary>
        /// Distances the specified text1.
        /// </summary>
        /// <param name="text1">The text1.</param>
        /// <param name="text2">The text2.</param>
        /// <returns>Levenstein Distance.</returns>
        public static int Distance(string text1, string text2)
        {
            if (text1 == null || text2 == null)
            {
                return 0;
            }

            int text1Length = text1.Length;
            int text2Length = text2.Length;

            if (text1Length == 0)
            {
                return text2Length;
            }

            if (text2Length == 0)
            {
                return text1Length;
            }

            int[,] matrix = CreateMatrix(text1Length, text2Length);

            CalculateMatrix(matrix, text1Length, text2Length, text1, text2);
            return matrix[text1Length, text2Length];
        }

        /// <summary>
        /// Percentage the specified text1.
        /// </summary>
        /// <param name="text1">The text1.</param>
        /// <param name="text2">The text2.</param>
        /// <returns>double Levenstein Percentage.</returns>
        public static double Percentage(string text1, string text2)
        {
            int levDis = Distance(text1, text2);
            int bigger = Math.Max(text1.Length, text2.Length);
            double procent = (double)(bigger - levDis) / (double)bigger;

            return procent * 100;
        }

        /// <summary>
        /// Calculates the matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="xDimension">The x dimension.</param>
        /// <param name="yDimension">The y dimension.</param>
        /// <param name="text1">The text1.</param>
        /// <param name="text2">The text2.</param>
        private static void CalculateMatrix(int[,] matrix, int xDimension, int yDimension, string text1, string text2)
        {
            for (int i = 1; i <= xDimension; i++)
            {
                for (int j = 1; j <= yDimension; j++)
                {
                    int cost = (text2[j - 1] == text1[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }
        }

        /// <summary>
        /// Creates the matrix.
        /// </summary>
        /// <param name="xDimension">The x dimension.</param>
        /// <param name="yDimension">The y dimension.</param>
        /// <returns>Initialized matrix</returns>
        private static int[,] CreateMatrix(int xDimension, int yDimension)
        {
            int[,] matrix = new int[xDimension + 1, yDimension + 1];

            for (int i = 0; i <= xDimension; i++)
            {
                matrix[i, 0] = i;
            }

            for (int j = 0; j <= yDimension; j++)
            {
                matrix[0, j] = j;
            }

            return matrix;
        }
    }
}
