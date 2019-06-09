// *********************************************************************************
// <copyright file="BidValidatorTests.cs" company="Transilvania University of Brasov">
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
    /// Class BidValidatorTests
    /// </summary>
    [TestFixture]
    public class BidValidatorTests
    {
        /// <summary>
        /// The bid
        /// </summary>
        private BidValidator bidValidator;

        /// <summary>
        /// The bid
        /// </summary>
        private Bid bid;

        /// <summary>
        /// Another bid
        /// </summary>
        private Bid anotherBid;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.bidValidator = new BidValidator();
            this.anotherBid = new Bid
            {
                Id = 1,
                Amount = 10,
                Date = DateTime.Now.AddDays(-1)
            };
            this.bid = new Bid
            {
                Id = 2,
                Amount = 10,
                Date = DateTime.Now,
                Product = new Product
                {
                    Id = 1,
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(10),
                    Name = "Valid Name for Product",
                    Currency = new Currency { Name = "ValidCurrency", Abbreviation = "VC" },
                    Price = 10,
                    Description = "This is a valid despription of a valid product because it has a valid lenght.",
                    Bid = new List<Bid> { this.anotherBid }
                }
            };
        }

        /// <summary>
        /// Bits the validator should consider bit valid when every property is valid.
        /// </summary>
        [Test]
        public void BidValidator_ShouldConsiderBitValid_WhenEveryProperyIsValid()
        {
            this.bid.Amount = 10.5;
            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Tests the method.
        /// </summary>
        [Test]
        public void BidValidator_ShouldNotConsiderBidValid_WhenBidHasNoProduct()
        {
            this.bid.Product = null;

            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Bids the validator should not consider bid valid when bid has invalid product.
        /// </summary>
        [Test]
        public void BidValidator_ShouldNotConsiderBidValid_WhenBidHasInvalidProduct()
        {
            this.bid.Product.EndDate = DateTime.Now.AddDays(-1);

            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Bids the validator should not consider bid valid when bid has invalid product currency.
        /// </summary>
        [Test]
        public void BidValidator_ShouldNotConsiderBidValid_WhenBidHasInvalidProductCurrency()
        {
            this.bid.Product.Currency = null;

            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Bids the validator should not consider bid valid when bid amount has negative value.
        /// </summary>
        [Test]
        public void BidValidator_ShouldNotConsiderBidValid_WhenBidAmountHasNegativeValue()
        {
            this.bid.Amount = -10;

            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Bids the validator should not consider bid valid when bid amount has a smaller value than the last one.
        /// </summary>
        [Test]
        public void BidValidator_ShouldNotConsiderBidValid_WhenBidAmountHasASmallerValueThanTheLastOne()
        {
            this.bid.Amount = 8;

            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Bids the validator should not consider bid valid when bid product has no other bids and amount is not greater than the product price.
        /// </summary>
        [Test]
        public void BidValidator_ShouldNotConsiderBidValid_WhenBidProductHasNoOtherBidsAndAmountIsNotGreaterThanTheProductPrice()
        {
            this.bid.Product.Bid = null;

            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Bids the validator should consider bid valid when bid amount has a greater value than the last one.
        /// </summary>
        [Test]
        public void BidValidator_ShouldConsiderBidValid_WhenBidAmountHasAGreaterValueThanTheLastOne()
        {
            this.bid.Amount = 11;

            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsTrue(validationResult.IsValid);
        }

        /// <summary>
        /// Bids the validator should not consider bid valid when product has different bid with same identifier.
        /// </summary>
        [Test]
        public void BidValidator_ShouldNotConsiderBidValid_WhenProductHasDifferentBidWithSameId()
        {
            this.anotherBid.Id = 2;
            this.bid.Amount = 11;

            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Bids the validator should not consider bid valid when date is in past.
        /// </summary>
        [Test]
        public void BidValidator_ShouldNotConsiderBidValid__WhenDateIsInPast()
        {
            this.bid.Date = DateTime.Now.AddSeconds(-1);

            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsFalse(validationResult.IsValid);
        }

        /// <summary>
        /// Bids the validator should not consider bid valid when date is in future.
        /// </summary>
        [Test]
        public void BidValidator_ShouldNotConsiderBidValid__WhenDateIsInFuture()
        {
            this.bid.Date = DateTime.Now.AddSeconds(1);

            var validationResult = this.bidValidator.Validate(this.bid);

            Assert.IsFalse(validationResult.IsValid);
        }
    }
}
