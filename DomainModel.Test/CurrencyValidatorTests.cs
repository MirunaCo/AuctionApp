// *********************************************************************************
// <copyright file="CurrencyValidatorTests.cs" company="Transilvania University of Brasov">
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
    /// Class CurrencyValidatorTests
    /// </summary>
    [TestFixture]
    public class CurrencyValidatorTests
    {
        /// <summary>
        /// The currency validator
        /// </summary>
        private CurrencyValidator currencyValidator;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.currencyValidator = new CurrencyValidator();
        }

        /// <summary>
        /// Currencies the validator should consider currency valid when name is not empty has more than four characters and abbreviation is not empty.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldConsiderCurrencyValid_WhenNameIsNotEmptyHasMoreThanFourCharactersAndAbbreviationIsNotEmpty()
        {
            var currency = new Currency { Name = "ValidCurrency", Abbreviation = "VC" };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should not consider currency valid when name is not empty has more than four characters and abbreviation is empty.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldNotConsiderCurrencyValid_WhenNameIsNotEmptyHasMoreThanFourCharactersAndAbbreviationIsEmpty()
        {
            var currency = new Currency { Name = "ValidCurrency", Abbreviation = string.Empty };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should not consider currency valid when name is not empty has more than four characters and abbreviation is null.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldNotConsiderCurrencyValid_WhenNameIsNotEmptyHasMoreThanFourCharactersAndAbvreviationIsNull()
        {
            var currency = new Currency { Name = "ValidCurrency", Abbreviation = null };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should not consider currency valid when name is not empty has more than four characters and abbreviation is white spaces.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldNotConsiderCurrencyValid_WhenNameIsNotEmptyHasMoreThanFourCharactersAndAbbreviationIsWhiteSpaces()
        {
            var currency = new Currency { Name = "ValidCurrency", Abbreviation = "           " };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should not consider currency valid when name is empty.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldNotConsiderCurrencyValid_WhenNameIsEmpty()
        {
            var currency = new Currency { Name = string.Empty };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should not consider currency valid when name is white spaces.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldNotConsiderCurrencyValid_WhenNameIsWhiteSpaces()
        {
            var currency = new Currency { Name = "              " };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should not consider currency valid when name has less than four characters.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldNotConsiderCurrencyValid_WhenNameHasLessThanFourCharacters()
        {
            var currency = new Currency { Name = "abc" };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should  consider currency valid when name than four characters.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldConsiderCurrencyValid_WhenNameHasFourCharacters()
        {
            var currency = new Currency { Name = "abcd", Abbreviation = "ab" };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should consider currency valid when name than four characters.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShoulConsiderCurrencyValid_WhenNameHasFiftyCharacters()
        {
            var currency = new Currency { Name = "abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde", Abbreviation = "Ab" };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should  consider currency valid when abbreviation has one characters.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldConsiderCurrencyValid_WhenAbreviationHasOneCharacters()
        {
            var currency = new Currency { Abbreviation = "a", Name = "Abcde" };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should consider currency valid when abbreviation has ten characters.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShoulConsiderCurrencyValid_WhenabreviationHasTenCharacters()
        {
            var currency = new Currency { Abbreviation = "abcdeabcde", Name = "super" };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should not consider currency valid when name has more than fifty characters.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldNotConsiderCurrencyValid_WhenNameHasMoreThanFiftyCharacters()
        {
            var currency = new Currency { Name = "ThisShouldNotBeAValidCurrencySinceItHasSoManyCharactesAndDataBaseWillNotBeAbleToRegisterIt." };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should not consider currency valid when name is null.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldNotConsiderCurrencyValid_WhenNameIsNull()
        {
            var currency = new Currency { Name = null };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Currencies the validator should not consider currency valid when name is valid but abbreviation is too long.
        /// </summary>
        [Test]
        public void CurrencyValidator_ShouldNotConsiderCurrencyValid_WhenNameIsValidButAbbreviationIsTooLong()
        {
            var currency = new Currency { Name = "validName", Abbreviation = "InvalidAbreviation" };

            var validationResult = this.currencyValidator.Validate(currency);

            Assert.IsFalse(validationResult.IsValid);
        }
    }
}
