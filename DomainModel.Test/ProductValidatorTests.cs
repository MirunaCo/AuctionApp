// *********************************************************************************
// <copyright file="ProductValidatorTests.cs" company="Transilvania University of Brasov">
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
    /// Class ProductValidatorTests.
    /// </summary>
    [TestFixture]
    public class ProductValidatorTests
    {
        /// <summary>
        /// The product validator
        /// </summary>
        private ProductValidator productValidator;

        /// <summary>
        /// The product
        /// </summary>
        private Product product;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.productValidator = new ProductValidator();
            this.product = new Product
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(10),
                Name = "Valid Name for Product",
                Currency = new Currency { Name = "ValidCurrency", Abbreviation = "VC" },
                Price = 10,
                Description = "This is a valid despription of a valid product because it has a valid lenght."
            };
        }

        /// <summary>
        /// Products the validator should consider product valid when start date is not in the past and before end date and all the other fields are in valid length.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldConsiderProductValid_WhenStartDateIsNotInThePastAndBeforeEndDateAndAllTheOtherFieldsAreInValidLength()
        {
            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when start date is in the past.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenStartDateIsInThePast()
        {
            this.product.StartDate = DateTime.Now.AddDays(-1);
            var errorMessage = "Start date can't be in past.";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors[0].ErrorMessage, errorMessage);
        }

        /// <summary>
        /// Products the validator should not consider product valid when end date is in the past.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenEndDateIsInThePast()
        {
            this.product.EndDate = DateTime.Now.AddDays(-1);
            var errorMessage = "Start date should be greater than End date.";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors[0].ErrorMessage, errorMessage);
        }

        /// <summary>
        /// Products the validator should not consider product valid when start date greater than the end date.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenStartDateGreaterThanTheEndDate()
        {
            this.product.StartDate = DateTime.Now.AddDays(11);
            var errorMessage = "Start date should be greater than End date.";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors[0].ErrorMessage, errorMessage);
        }

        /// <summary>
        /// Products the validator should not consider product valid when currency is not valid.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenCurrencyIsNotValid()
        {
            this.product.Currency = new Currency { Name = string.Empty, Abbreviation = string.Empty };

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when currency is null.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenCurrencyIsNull()
        {
            this.product.Currency = new Currency();

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when description is empty.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenDescriptionIsEmpty()
        {
            this.product.Description = string.Empty;

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when description is white spaces.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenDescriptionIsWhiteSpaces()
        {
            this.product.Description = "         ";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when description is null.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenDescriptionIsNull()
        {
            this.product.Description = null;

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when description too short.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenDescriptionTooShort()
        {
            this.product.Description = "invalid description";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should consider product valid when description has forty nine characters.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldConsiderProductValid_WhenDescriptionHasForthyNineCharacters()
        {
            this.product.Description = "descriptiondescriptiondescriptiondescriptiondescr";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when description too long.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenDescriptionTooLong()
        {
            this.product.Description = "invalid description invalid description invalid descriptioninvalid" +
                                   "description invalid description invalid description invalid description" +
                                   "description invalid description invalid description invalid description" +
                                   "description invalid description invalid description invalid description" +
                                   "description invalid description invalid description invalid description";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when name is empty.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenNameIsEmpty()
        {
            this.product.Name = string.Empty;

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when name is null.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenNameIsNull()
        {
            this.product.Name = null;

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when name is white spaces.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenNameIsWhiteSpaces()
        {
            this.product.Name = "      ";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when name too short.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenNameTooShort()
        {
            this.product.Name = "invalid";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should consider product valid when name has nine characters.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldConsiderProductValid_WhenNameHasNineCharacters()
        {
            this.product.Name = "invalidpr";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should consider product valid when name has forty-nine characters.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldConsiderProductValid_WhenNameHasFourtyNineCharacters()
        {
            this.product.Name = "invalidprinvalidprinvalidprinvalidprinvalidprinva";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when name too long.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenNameTooLong()
        {
            this.product.Name = "invalid name invalid name invalid name invalid name invalid name invalid name";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Products the validator should not consider product valid when price has negative value.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderProductValid_WhenPriceHasNegativeValue()
        {
            this.product.Price = -10;
            var errorMessage = "Price should be greater then 0.";

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors[0].ErrorMessage, errorMessage);
        }

        /// <summary>
        /// Products the validator should consider product valid when price is zero.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldConsiderProductNotValid_WhenPriceIsZero()
        {
            this.product.Price = 0;

            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Product the validator should consider product valid when currency name is not empty has more than four characters and abbreviation is not empty.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldConsiderCurrencyValid_WhenNameIsNotEmptyHasMoreThanFourCharactersAndAbbreviationIsNotEmpty()
        {
            var currency = new Currency { Name = "ValidCurrency", Abbreviation = "VC" };
            this.product.Currency = currency;
            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Product the validator should not consider product valid when currency name is not empty has no characters and abbreviation is not empty.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderCurrencyValid_WhenNameIsNotEmptyHasNoCharactersAndAbbreviationIsNotEmpty()
        {
            var currency = new Currency { Name = string.Empty, Abbreviation = "VC" };
            this.product.Currency = currency;
            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Product the validator should not consider product valid when currency name is not empty has more than four characters and abbreviation is empty.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderCurrencyValid_WhenNameIsNotEmptyHasMoreThanFourCharactersAndAbbreviationIsEmpty()
        {
            var currency = new Currency { Name = "ValidCurrency", Abbreviation = string.Empty };
            this.product.Currency = currency;
            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Product the validator should not consider product valid when category name is empty.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldNotConsiderCategoryValid_WhenNameIsEmpty()
        {
            this.product.Category = new Category { Name = string.Empty };
            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Product the validator should consider product valid when category name is not empty.
        /// </summary>
        [Test]
        public void ProductValidator_ShouldConsiderCurrencyValid_WhenNameIsNotEmpty()
        {
            var category = new Category { Name = "Category" };
            this.product.Category = category;
            var validationResult = this.productValidator.Validate(this.product);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
