// ***********************************************************************
// <copyright file="ScoreServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Class ScoreServiceTests
    /// </summary>
    [TestFixture]
    public class ScoreServiceTests
    {
        /// <summary>
        /// The score service
        /// </summary>
        private ScoreServices scoreService;

        /// <summary>
        /// The score repository
        /// </summary>
        private IRepositoryScore scoreRepository;

        /// <summary>
        /// The score
        /// </summary>
        private Score score;

        /// <summary>
        /// The user service
        /// </summary>
        private IUserServices userService;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.score = new Score
            {
                Id = 1,
                Points = 5,
                UserIdFrom = 1,
                UserFrom = new User { Email = "from@valid.com", Password = "123456", Username = "userFrom" },
                User = new User { Email = "to@valid.com", Password = "123456", Username = "userTo" },
                UserIdTo = 2
            };
            this.scoreRepository = A.Fake<IRepositoryScore>();
            this.userService = A.Fake<IUserServices>();
            this.scoreService = new ScoreServices(this.scoreRepository, this.userService);
        }

        /// <summary>
        /// Deletes the should invoke score repository with given identifier.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeScoreRepository_WithGivenId()
        {
            this.scoreService.Delete(this.score.Id);

            A.CallTo(() => this.scoreRepository.Delete(this.score.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Deletes the should invoke score repository with given score.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeScoreRepository_WithGivenScore()
        {
            this.scoreService.Delete(this.score);

            A.CallTo(() => this.scoreRepository.Delete(this.score)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke score repository with given filter order and properties.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeScoreRepository_WithGivenFilterOrderAndProperties()
        {
            Expression<Func<Score, bool>> expression = x => x.Points == 2;

            var currencies = this.scoreService.Get(expression, null, "Id");

            A.CallTo(() => this.scoreRepository.Get(expression, null, "Id")).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke score repository with given filter order and properties and return a not null value.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeScoreRepository_WithGivenFilterOrderAndPropertiesAndReturnANotNullValue()
        {
            Expression<Func<Score, bool>> expression = x => x.Points == 2;

            var currencies = this.scoreService.Get(expression, null, "Id");

            Assert.IsNotNull(currencies);
        }

        /// <summary>
        /// Gets the by identifier should invoke score repository with given score identifier.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeScoreRepository_WithGivenScoreId()
        {
            var score = this.scoreService.GetByID(this.score.Id);

            A.CallTo(() => this.scoreRepository.GetByID(this.score.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the by identifier should invoke score repository with given score identifier and return a not null value.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeScoreRepository_WithGivenScoreIdAndReturnANotNullValue()
        {
            var score = this.scoreService.GetByID(this.score.Id);

            Assert.IsNotNull(score);
        }

        /// <summary>
        /// Inserts the should invoke score repository with given score.
        /// </summary>
        [Test]
        public void Insert_ShouldInvokeScoreRepository_WithGivenScore()
        {
            this.scoreService.Insert(this.score);

            A.CallTo(() => this.scoreRepository.Insert(this.score)).MustHaveHappened();
        }

        /// <summary>
        /// Inserts the should throw invalid score exception when given score is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidScoreException_WhenGivenScoreIsNotValid()
        {
            this.score.Points = -1;

            Assert.Throws<InvalidScoreException>(() => this.scoreService.Insert(this.score));
        }

        /// <summary>
        /// Inserts the should throw invalid score exception when given score is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidScoreExceptioneWithSpecificMessageException_WhenGivenScoreIsNotValid()
        {
            this.score.Points = -1;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidScoreException' was thrown.";

            var exception = Assert.Throws<InvalidScoreException>(() => this.scoreService.Insert(this.score));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Inserts the should throw invalid score exception when given user to is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidScoreException_WhenGivenUserToIsNotValid()
        {
            this.score.User = new User();

            Assert.Throws<InvalidScoreException>(() => this.scoreService.Insert(this.score));
        }

        /// <summary>
        /// Inserts the should throw invalid score exception when given user to is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidScoreExceptioneWithSpecificMessageException_WhenGivenUserToIsNotValid()
        {
            this.score.User = new User();
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidScoreException' was thrown.";

            var exception = Assert.Throws<InvalidScoreException>(() => this.scoreService.Insert(this.score));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Inserts the should throw invalid score exception when given user from is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidScoreException_WhenGivenUserFromIsNotValid()
        {
            this.score.UserFrom = new User();

            Assert.Throws<InvalidScoreException>(() => this.scoreService.Insert(this.score));
        }

        /// <summary>
        /// Inserts the should throw invalid score exception when given user from is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidScoreExceptioneWithSpecificMessageException_WhenGivenUserFromIsNotValid()
        {
            this.score.UserFrom = new User();
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidScoreException' was thrown.";

            var exception = Assert.Throws<InvalidScoreException>(() => this.scoreService.Insert(this.score));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should invoke score repository with given score.
        /// </summary>
        [Test]
        public void Update_ShouldInvokeScoreRepository_WithGivenScore()
        {
            this.scoreService.Update(this.score);

            A.CallTo(() => this.scoreRepository.Update(this.score)).MustHaveHappened();
        }

        /// <summary>
        /// Updates the should throw invalid score exception when given score is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidScoreException_WhenGivenScoreIsNotValid()
        {
            this.score.Points = -1;

            Assert.Throws<InvalidScoreException>(() => this.scoreService.Update(this.score));
        }

        /// <summary>
        /// Updates the should throw invalid score exception when given score is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidScoreWithSpecificMessageException_WhenGivenScoreIsNotValid()
        {
            this.score.Points = -1;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidScoreException' was thrown.";

            var exception = Assert.Throws<InvalidScoreException>(() => this.scoreService.Update(this.score));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should throw invalid score exception when given user to is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidScoreException_WhenGivenUserToIsNotValid()
        {
            this.score.User = new User();

            Assert.Throws<InvalidScoreException>(() => this.scoreService.Update(this.score));
        }

        /// <summary>
        /// Updates the should throw invalid score exception when given user to is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidScoreWithSpecificMessageException_WhenGivenUserToIsNotValid()
        {
            this.score.User = new User();
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidScoreException' was thrown.";

            var exception = Assert.Throws<InvalidScoreException>(() => this.scoreService.Update(this.score));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should throw invalid score exception when given user from is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidScoreException_WhenGivenUserFromIsNotValid()
        {
            this.score.UserFrom = new User();

            Assert.Throws<InvalidScoreException>(() => this.scoreService.Update(this.score));
        }

        /// <summary>
        /// Updates the should throw invalid score exception when given user from is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidScoreWithSpecificMessageException_WhenGivenUserFromIsNotValid()
        {
            this.score.UserFrom = new User();
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidScoreException' was thrown.";

            var exception = Assert.Throws<InvalidScoreException>(() => this.scoreService.Update(this.score));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }
    }
}
