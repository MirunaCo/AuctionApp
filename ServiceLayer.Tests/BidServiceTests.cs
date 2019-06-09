// ***********************************************************************
// <copyright file="BidServiceTests.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// ***********************************************************************
namespace ServiceLayer.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using DataMapper;
    using DomainModel;
    using FakeItEasy;
    using NUnit.Framework;
    using ServiceLayer.Exception;
    using ServiceLayer.ServiceInterface;

    /// <summary>
    /// Class BidServiceTests
    /// </summary>
    [TestFixture]
    public class BidServiceTests
    {
        /// <summary>
        /// The bid service
        /// </summary>
        private IBidServices bidService;

        /// <summary>
        /// The bid repository
        /// </summary>
        private IRepositoryBid bidRepository;

        /// <summary>
        /// The bid
        /// </summary>
        private Bid bid;

        /// <summary>
        /// The another bid
        /// </summary>
        private Bid anotherBid;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.anotherBid = new Bid
            {
                Id = 1,
                Amount = 10,
                Date = DateTime.Now.AddDays(-1)
            };
            this.bid = new Bid
            {
                Id = 2,
                Amount = 10.5f,
                Date = DateTime.Now,
                Product = new Product
                {
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(10),
                    Name = "Valid Name for Product",
                    Currency = new Currency { Name = "ValidCurrency", Abbreviation = "VC" },
                    Price = 10,
                    Description = "This is a valid despription of a valid product because it has a valid lenght.",
                    Bid = new List<Bid> { this.anotherBid }
                }
            };
            this.bidRepository = A.Fake<IRepositoryBid>();
            this.bidService = new BidServices(this.bidRepository);
        }

        /// <summary>
        /// Deletes the should invoke bid repository with given identifier.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeBidRepository_WithGivenId()
        {
            this.bidService.Delete(this.bid.Id);

            A.CallTo(() => this.bidRepository.Delete(this.bid.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Deletes the should invoke bid repository with given category.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeBidRepository_WithGivenCategory()
        {
            this.bidService.Delete(this.bid);

            A.CallTo(() => this.bidRepository.Delete(this.bid)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke bid repository with given filter order and properties.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeBidRepository_WithGivenFilterOrderAndProperties()
        {
            Expression<Func<Bid, bool>> expression = x => x.User.Username.Contains("computer");

            var bid = this.bidService.Get(expression, null, "Id");

            A.CallTo(() => this.bidRepository.Get(expression, null, "Id")).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke bid repository with given filter order and properties and return a not null value.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeBidRepository_WithGivenFilterOrderAndPropertiesAndReturnANotNullValue()
        {
            Expression<Func<Bid, bool>> expression = x => x.User.Username.Contains("computer");

            var bid = this.bidService.Get(expression, null, "Id");

            Assert.IsNotNull(bid);
        }

        /// <summary>
        /// Gets the by identifier should invoke bid repository with given bid identifier.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeCategoryRepository_WithGivenCategoryId()
        {
            var bid = this.bidService.GetByID(this.bid.Id);

            A.CallTo(() => this.bidRepository.GetByID(this.bid.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the by identifier should invoke category repository with given category identifier return a not null value.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeCategoryRepository_WithGivenCategoryIdReturnANotNullValue()
        {
            var bid = this.bidService.GetByID(this.bid.Id);

            Assert.IsNotNull(bid);
        }

        /// <summary>
        /// Inserts the should invoke bid repository with given category.
        /// </summary>
        [Test]
        public void Insert_ShouldInvokeBidRepository_WithGivenCategory()
        {
            this.bidService.Insert(this.bid);

            A.CallTo(() => this.bidRepository.Insert(this.bid)).MustHaveHappened();
        }

        /// <summary>
        /// Inserts the should throw invalid bid exception when given bid is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidBidException_WhenGivenBidIsNotValid()
        {
            this.bid.Date = DateTime.Now.AddDays(-1);

            Assert.Throws<InvalidBidException>(() => this.bidService.Insert(this.bid));
        }

        /// <summary>
        /// Inserts the should throw invalid bid exception with specific message exception when given bid is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidBidExceptionWithSpecificMessageException_WhenGivenBidIsNotValid()
        {
            this.bid.Date = DateTime.Now.AddDays(-1);
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidBidException' was thrown.";

            var exception = Assert.Throws<InvalidBidException>(() => this.bidService.Insert(this.bid));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should invoke bid repository with given bid.
        /// </summary>
        [Test]
        public void Update_ShouldInvokeBidRepository_WithGivenCategory()
        {
            this.bidService.Update(this.bid);

            A.CallTo(() => this.bidRepository.Update(this.bid)).MustHaveHappened();
        }

        /// <summary>
        /// Updates the should throw invalid bid exception when given bid is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidBidException_WhenGivenBidIsNotValid()
        {
            this.bid.Date = DateTime.Now.AddDays(-1);

            Assert.Throws<InvalidBidException>(() => this.bidService.Update(this.bid));
        }

        /// <summary>
        /// Updates the should throw invalid bid exception with specific message exception when given bid is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidBidExceptionWithSpecificMessageException_WhenGivenBidIsNotValid()
        {
            this.bid.Date = DateTime.Now.AddDays(-1);
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidBidException' was thrown.";

            var exception = Assert.Throws<InvalidBidException>(() => this.bidService.Update(this.bid));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }
    }
}
