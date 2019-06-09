// *********************************************************************************
// <copyright file="UserValidatorTests.cs" company="Transilvania University of Brasov">
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
    /// Class UserValidatorTests.
    /// </summary>
    [TestFixture]
    public class UserValidatorTests
    {
        /// <summary>
        /// The user validator
        /// </summary>
        private UserValidator userValidator;

        /// <summary>
        /// The user
        /// </summary>
        private User user;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.userValidator = new UserValidator();
            this.user = new User { Email = "valid@valid.com", Password = "123456", Username = "validUserName" };
        }

        /// <summary>
        /// Users the validator should consider user valid when email is valid and all the other fields are in valid length.
        /// </summary>
        [Test]
        public void UserValidator_ShouldConsiderUserValid_WhenEmailIsValidAndAllTheOtherFieldsAreInValidLenght()
        {
            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when email is not valid.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenEmailIsNotValid()
        {
            this.user.Email = "a.com";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when email is empty.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenEmailIsEmpty()
        {
            this.user.Email = string.Empty;

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when email is null.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenEmailIsNull()
        {
            this.user.Email = null;

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when email is white spaces.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenEmailIsWhiteSpaces()
        {
            this.user.Email = "        ";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when email is without @.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenEmailIsWithoutArond()
        {
            this.user.Email = "emailinvalid";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when password is empty.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenPasswordIsEmpty()
        {
            this.user.Password = string.Empty;

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when password is null.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenPasswordIsNull()
        {
            this.user.Password = null;

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when password is white spaces.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenPasswordIsWhiteSpaces()
        {
            this.user.Password = "        ";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when password is too short.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenPasswordIsTooShort()
        {
            this.user.Password = "abc";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when password is too long.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenPasswordIsTooLong()
        {
            this.user.Password = "this password is obviously too long, isn't it?";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should consider user valid when password has exactly six characters.
        /// </summary>
        [Test]
        public void UserValidator_ShouldConsiderUserValid_WhenPasswordHasExactlySixCharacters()
        {
            this.user.Password = "abcdef";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should consider user valid when password has exactly twenty characters.
        /// </summary>
        [Test]
        public void UserValidator_ShouldConsiderUserValid_WhenPasswordHasExactlyTwentyCharacters()
        {
            this.user.Password = "this password has 20";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when username is empty.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenUsernameIsEmpty()
        {
            this.user.Username = string.Empty;

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when username is null.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenUsernameIsNull()
        {
            this.user.Username = null;

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when usernames white spaces.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenUsernamesWhiteSpaces()
        {
            this.user.Username = "        ";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when p username is too short.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenPUsernameIsTooShort()
        {
            this.user.Username = "abc";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should not consider user valid when username is too long.
        /// </summary>
        [Test]
        public void UserValidator_ShouldNotConsiderUserValid_WhenUsernameIsTooLong()
        {
            this.user.Password = "this user name is obviously too long, isn't it?";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should consider user valid when username has exactly six characters.
        /// </summary>
        [Test]
        public void UserValidator_ShouldConsiderUserValid_WhenUsernameHasExactlySixCharacters()
        {
            this.user.Username = "abcdef";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Users the validator should consider user valid when username has exactly twenty characters.
        /// </summary>
        [Test]
        public void UserValidator_ShouldConsiderUserValid_WhenUsernameHasExactlyTwentyCharacters()
        {
            this.user.Username = "this username has 20";

            var validationResult = this.userValidator.Validate(this.user);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
