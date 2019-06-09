// ***********************************************************************
// <copyright file="CurrencyServiceTests.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// ***********************************************************************
namespace ServiceLayer.Tests
{
    using System;
    using System.Linq.Expressions;
    using DataMapper;
    using DomainModel;
    using FakeItEasy;
    using NUnit.Framework;
    using ServiceLayer.Exception;
    using ServiceLayer.ServiceInterface;

    /// <summary>
    /// Class CurrencyServiceTests
    /// </summary>
    [TestFixture]
    public class CurrencyServiceTests
    {
        /// <summary>
        /// The currency service
        /// </summary>
        private ICurrencyServices currencyService;

        /// <summary>
        /// The currency repository
        /// </summary>
        private IRepositoryCurrency currencyRepository;

        /// <summary>
        /// The currency
        /// </summary>
        private Currency currency;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.currency = new Currency { Name = "ValidCurrency", Abbreviation = "VC" };
            this.currencyRepository = A.Fake<IRepositoryCurrency>();
            this.currencyService = new CurrencyServices(this.currencyRepository);
        }

        /// <summary>
        /// Deletes the should invoke currency repository with given identifier.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeCurrencyRepository_WithGivenId()
        {
            this.currencyService.Delete(this.currency.Id);

            A.CallTo(() => this.currencyRepository.Delete(this.currency.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Deletes the should invoke currency repository with given currency.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeCurrencyRepository_WithGivenCurrency()
        {
            this.currencyService.Delete(this.currency);

            A.CallTo(() => this.currencyRepository.Delete(this.currency)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke currency repository with given filter order and properties.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeCurrencyRepository_WithGivenFilterOrderAndProperties()
        {
            Expression<Func<Currency, bool>> expression = x => x.Name.Length == 2;

            var currencies = this.currencyService.Get(expression, null, "Id");

            A.CallTo(() => this.currencyRepository.Get(expression, null, "Id")).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke currency repository with given filter order and properties and return a not null value.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeCurrencyRepository_WithGivenFilterOrderAndPropertiesAndReturnANotNullValue()
        {
            Expression<Func<Currency, bool>> expression = x => x.Name.Length == 2;

            var currencies = this.currencyService.Get(expression, null, "Id");

            Assert.IsNotNull(currencies);
        }

        /// <summary>
        /// Gets the by identifier should invoke currency repository with given currency identifier.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeCurrencyRepository_WithGivenCurrencyId()
        {
            var currency = this.currencyService.GetByID(this.currency.Id);

            A.CallTo(() => this.currencyRepository.GetByID(this.currency.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the by identifier should invoke currency repository with given currency identifier and return a not null value.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeCurrencyRepository_WithGivenCurrencyIdAndReturnANotNullValue()
        {
            var currency = this.currencyService.GetByID(this.currency.Id);

            Assert.IsNotNull(currency);
        }

        /// <summary>
        /// Inserts the should invoke currency repository with given currency.
        /// </summary>
        [Test]
        public void Insert_ShouldInvokeCurrencyRepository_WithGivenCurrency()
        {
            this.currencyService.Insert(this.currency);

            A.CallTo(() => this.currencyRepository.Insert(this.currency)).MustHaveHappened();
        }

        /// <summary>
        /// Inserts the should throw invalid currency exception when given currency is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidCurrencyException_WhenGivenCurrencyIsNotValid()
        {
            this.currency.Name = string.Empty;

            Assert.Throws<InvalidCurrencyException>(() => this.currencyService.Insert(this.currency));
        }

        /// <summary>
        /// Inserts the should throw invalid currency exception with specific message exception when given currency is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidCurrencyExceptionWithSpecificMessageException_WhenGivenCurrencyIsNotValid()
        {
            this.currency.Abbreviation = string.Empty;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidCurrencyException' was thrown.";

            var exception = Assert.Throws<InvalidCurrencyException>(() => this.currencyService.Insert(this.currency));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Inserts the should throw invalid currency exception when given currency name is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidCurrencyException_WhenGivenCurrencyNameIsNotValid()
        {
            this.currency.Name = "name has more than fifty charactersname has more than fifty characters";

            Assert.Throws<InvalidCurrencyException>(() => this.currencyService.Insert(this.currency));
        }

        /// <summary>
        /// Inserts the should throw invalid currency exception with specific message exception when given currency abbreviation is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidCurrencyExceptionWithSpecificMessageException_WhenGivenCurrencyAbbreviationIsNotValid()
        {
            this.currency.Abbreviation = "abbreviation";
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidCurrencyException' was thrown.";

            var exception = Assert.Throws<InvalidCurrencyException>(() => this.currencyService.Insert(this.currency));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should invoke currency repository with given currency.
        /// </summary>
        [Test]
        public void Update_ShouldInvokeCurrencyRepository_WithGivenCurrency()
        {
            this.currencyService.Update(this.currency);

            A.CallTo(() => this.currencyRepository.Update(this.currency)).MustHaveHappened();
        }

        /// <summary>
        /// Updates the should throw invalid currency exception when given currency is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidCurrencyException_WhenGivenCurrencyIsNotValid()
        {
            this.currency.Name = string.Empty;

            Assert.Throws<InvalidCurrencyException>(() => this.currencyService.Update(this.currency));
        }

        /// <summary>
        /// Updates the should throw invalid currency exception with specific message exception when given currency is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidCurrencyExceptionWithSpecificMessageException_WhenGivenCurrencyIsNotValid()
        {
            this.currency.Abbreviation = string.Empty;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidCurrencyException' was thrown.";

            var exception = Assert.Throws<InvalidCurrencyException>(() => this.currencyService.Update(this.currency));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should throw invalid currency exception when given currency name is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidCurrencyException_WhenGivenCurrencyNameIsNotValid()
        {
            this.currency.Name = "nam";

            Assert.Throws<InvalidCurrencyException>(() => this.currencyService.Update(this.currency));
        }

        /// <summary>
        /// Updates the should throw invalid currency exception with specific message exception when given currency abbreviation is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidCurrencyExceptionWithSpecificMessageException_WhenGivenCurrencyAbbreviationIsNotValid()
        {
            this.currency.Abbreviation = string.Empty;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidCurrencyException' was thrown.";

            var exception = Assert.Throws<InvalidCurrencyException>(() => this.currencyService.Update(this.currency));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }
    }
}
