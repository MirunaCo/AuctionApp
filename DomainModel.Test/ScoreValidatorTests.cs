// *********************************************************************************
// <copyright file="ScoreValidatorTests.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace DomainModel.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DomainModel.Validator;
    using NUnit.Framework;

    /// <summary>
    /// Class ScoreValidatorTests.
    /// </summary>
    [TestFixture]
    public class ScoreValidatorTests
    {
        /// <summary>
        /// The score validator
        /// </summary>
        private ScoreValidator scoreValidator;

        /// <summary>
        /// The score
        /// </summary>
        private Score score;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.scoreValidator = new ScoreValidator();
            this.score = new Score
            {
                Id = 1,
                Points = 5d,
                UserFrom = new User { Id = 1, Email = "from@valid.com", Password = "123456", Username = "userFrom" },
                UserIdFrom = 1,
                User = new User { Id = 2, Email = "to@valid.com", Password = "123456", Username = "userTo" },
                UserIdTo = 2
            };
        }

        /// <summary>
        /// Scores the validator should consider score valid when it has from and to users and valid value.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldConsiderScoreValid_WhenItHasFromAndToUsersAndValidValue()
        {
            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when it has no from user.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenItHasNoFromUser()
        {
            this.score.UserFrom = null;
            this.score.UserIdFrom = 0;

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when from user is not valid.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenFromUserIsNotValid()
        {
            this.score.UserFrom = new User();

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when from user email is not valid.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenFromUserEmailIsNotValid()
        {
            this.score.UserFrom = new User
            {
                Email = "abcdef"
            };

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when from user password is not valid.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenFromUserPasswordIsNotValid()
        {
            this.score.UserFrom = new User
            {
                Password = "          "
            };

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when from user username is not valid.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenFromUserUsernameIsNotValid()
        {
            this.score.UserFrom = new User
            {
                Username = "       "
            };

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when it has no to user.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenItHasNoToUser()
        {
            this.score.User = null;
            this.score.UserIdTo = 0;

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when to user is not valid.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenToUserIsNotValid()
        {
            this.score.User = new User();

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when to user email is not valid.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenToUserEmailIsNotValid()
        {
            this.score.User = new User
            {
                Email = "abcdef"
            };

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when to user password is not valid.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenToUserPasswordIsNotValid()
        {
            this.score.User = new User
            {
                Password = "          "
            };

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when to user username is not valid.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenToUserUsernameIsNotValid()
        {
            this.score.User = new User
            {
                Username = "       "
            };

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should not consider score valid when points value is negative.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldNotConsiderScoreValid_WhenPointsValueIsNegative()
        {
            this.score.Points = -1;

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should consider score valid when points value is zero.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldConsiderScoreValid_WhenPointsValueIsZero()
        {
            this.score.Points = 0d;

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Scores the validator should consider score valid when points value is zero.
        /// </summary>
        [Test]
        public void ScoreValidator_ShouldConsiderScoreValid_WhenPointsValueIsFive()
        {
            this.score.Points = 5d;

            var validationResult = this.scoreValidator.Validate(this.score);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
