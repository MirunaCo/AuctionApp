// *********************************************************************************
// <copyright file="CategoryValidatorTests.cs" company="Transilvania University of Brasov">
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
    /// Class CategoryValidatorTests
    /// </summary>
    [TestFixture]
    public class CategoryValidatorTests
    {
        /// <summary>
        /// The category validator
        /// </summary>
        private CategoryValidator categoryValidator;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.categoryValidator = new CategoryValidator();
        }

        /// <summary>
        /// Categories the validator should consider category valid when name is not empty and has more than four characters.
        /// </summary>
        [Test]
        public void CategoryValidator_ShouldConsiderCategoryValid_WhenNameIsNotEmptyAndHasMoreThanFourCharacters()
        {
            var category = new Category { Name = "ValidCategory" };

            var validationResult = this.categoryValidator.Validate(category);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Categories the validator should not consider category valid when name is empty.
        /// </summary>
        [Test]
        public void CategoryValidator_ShouldNotConsiderCategoryValid_WhenNameIsEmpty()
        {
            var category = new Category { Name = string.Empty };

            var validationResult = this.categoryValidator.Validate(category);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Categories the validator should not consider category valid when name has no more than four characters.
        /// </summary>
        [Test]
        public void CategoryValidator_ShouldNotConsiderCategoryValid_WhenNameHasNoMoreThanFourCharacters()
        {
            var category = new Category { Name = "abc" };

            var validationResult = this.categoryValidator.Validate(category);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Categories the validator should not consider category valid when name has more than fifty characters.
        /// </summary>
        [Test]
        public void CategoryValidator_ShouldNotConsiderCategoryValid_WhenNameHasMoreThanFiftyCharacters()
        {
            var category = new Category { Name = "ThisShouldNotBeAValidCategorySinceItHasSoManyCharactesAndDataBaseWillNotBeAbleToRegisterIt." };

            var validationResult = this.categoryValidator.Validate(category);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Categories the validator should not consider category valid when name is null.
        /// </summary>
        [Test]
        public void CategoryValidator_ShouldNotConsiderCategoryValid_WhenNameIsNull()
        {
            var category = new Category { Name = null };

            var validationResult = this.categoryValidator.Validate(category);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Categories the validator should not consider category valid when name is white spaces.
        /// </summary>
        [Test]
        public void CategoryValidator_ShouldNotConsiderCategoryValid_WhenNameIsWhiteSpaces()
        {
            var category = new Category { Name = "          " };

            var validationResult = this.categoryValidator.Validate(category);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Categories the validator should consider category valid when name has four characters.
        /// </summary>
        [Test]
        public void CategoryValidator_ShouldConsiderCategoryValid_WhenNameHasFourCharacters()
        {
            var category = new Category { Name = "abcd" };

            var validationResult = this.categoryValidator.Validate(category);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Categories the validator should consider category valid when name has twenty characters.
        /// </summary>
        [Test]
        public void CategoryValidator_ShouldConsiderCategoryValid_WhenNameHasTwentyCharacters()
        {
            var category = new Category { Name = "abcdabcdabcdabcdabcd" };

            var validationResult = this.categoryValidator.Validate(category);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Categories the validator should consider category valid when name has fifty characters.
        /// </summary>
        [Test]
        public void CategoryValidator_ShouldConsiderCategoryValid_WhenNameHasFiftyCharacters()
        {
            var category = new Category { Name = "abcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdabcdab" };

            var validationResult = this.categoryValidator.Validate(category);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
