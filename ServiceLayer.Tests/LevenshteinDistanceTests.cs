// ***********************************************************************
// <copyright file="LevenshteinDistanceTests.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// ***********************************************************************
namespace ServiceLayer.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// the class LevenshteinDistanceTests
    /// </summary>
    [TestFixture]
    public class LevenshteinDistanceTests
    {
        /// <summary>
        /// The text1
        /// </summary>
        private const string Text1 = "Leve";

        /// <summary>
        /// The text2
        /// </summary>
        private const string Text2 = "Leven";

        /// <summary>
        /// The distance
        /// </summary>
        private const int Distance = 1;

        /// <summary>
        /// The percentage
        /// </summary>
        private const double Percentage = 80.0;

        /// <summary>
        /// Levenshtein the distance should return distance one when two string have one difference.
        /// </summary>
        [Test]
        public void LevenshteinDistance_ShouldReturnDistanceOne_WhenTwoStringHaveOneDifference()
        {
            Assert.That(LevensteinDistance.Distance(Text1, Text2), Is.EqualTo(Distance));
        }

        /// <summary>
        /// Levenshtein the percentage should return distance one when two string have80 percent.
        /// </summary>
        [Test]
        public void LevenshteinPercentage_ShouldReturnDistanceOne_WhenTwoStringHave80Percent()
        {
            Assert.That(LevensteinDistance.Percentage(Text1, Text2), Is.EqualTo(Percentage));
        }

        /// <summary>
        /// Levenshtein the distance should return distance first length when second string is empty.
        /// </summary>
        [Test]
        public void LevenshteinDistance_ShouldReturnDistanceFirstLength_WhenSecondStringIsEmpty()
        {
            Assert.That(LevensteinDistance.Distance(Text1, string.Empty), Is.EqualTo(Text1.Length));
        }

        /// <summary>
        /// Levenshtein the distance should return distance first length when second string is empty.
        /// </summary>
        [Test]
        public void LevenshteinDistance_ShouldReturnDistanceSecondLenght_WhenFirstStringIsEmpty()
        {
            Assert.That(LevensteinDistance.Distance(string.Empty, Text2), Is.EqualTo(Text2.Length));
        }

        /// <summary>
        /// Levenstein the distance should return distance zero when one parameter string is null.
        /// </summary>
        [Test]
        public void LevenshteinDistance_ShouldReturnDistanceZero_WhenOneParameterStringIsNull()
        {
            Assert.That(LevensteinDistance.Distance(null, Text2), Is.EqualTo(0));
        }
    }
}
