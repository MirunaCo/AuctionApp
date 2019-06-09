// ***********************************************************************
// <copyright file="UserServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Class UserServiceTests
    /// </summary>
    [TestFixture]
    public class UserServiceTests
    {
        /// <summary>
        /// The user service
        /// </summary>
        private UserServices userService;

        /// <summary>
        /// The user repository
        /// </summary>
        private IRepositoryUser userRepository;

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
            this.user = new User { Email = "valid@valid.com", Password = "123456", Username = "validUserName" };
            this.userRepository = A.Fake<IRepositoryUser>();
            this.userService = A.Fake<UserServices>();
            this.userService = new UserServices(this.userRepository);
        }

        /// <summary>
        /// Deletes the should invoke user repository with given identifier.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeUserRepository_WithGivenId()
        {
            this.userService.Delete(this.user.Id);

            A.CallTo(() => this.userRepository.Delete(this.user.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Deletes the should invoke user repository with given user.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeUserRepository_WithGivenUser()
        {
            this.userService.Delete(this.user);

            A.CallTo(() => this.userRepository.Delete(this.user)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke user repository with given filter order and properties.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeUserRepository_WithGivenFilterOrderAndProperties()
        {
            Expression<Func<User, bool>> expression = x => x.Email.Contains("@gmail.com");

            var currencies = this.userService.Get(expression, null, "Id");

            A.CallTo(() => this.userRepository.Get(expression, null, "Id")).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke user repository with given filter order and properties and return a not null value.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeUserRepository_WithGivenFilterOrderAndPropertiesAndReturnANotNullValue()
        {
            Expression<Func<User, bool>> expression = x => x.Email.Contains("@gmail.com");

            var currencies = this.userService.Get(expression, null, "Id");

            Assert.IsNotNull(currencies);
        }

        /// <summary>
        /// Gets the by identifier should invoke user repository with given user identifier.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeUserRepository_WithGivenUserId()
        {
            var user = this.userService.GetByID(this.user.Id);

            A.CallTo(() => this.userRepository.GetByID(this.user.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the by identifier should invoke user repository with given user identifier and return a not null value.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeUserRepository_WithGivenUserIdAndReturnANotNullValue()
        {
            var user = this.userService.GetByID(this.user.Id);

            Assert.IsNotNull(user);
        }

        /// <summary>
        /// Inserts the should invoke user repository with given user.
        /// </summary>
        [Test]
        public void Insert_ShouldInvokeUserRepository_WithGivenUser()
        {
            this.userService.Insert(this.user);

            A.CallTo(() => this.userRepository.Insert(this.user)).MustHaveHappened();
        }

        /// <summary>
        /// Inserts the should throw invalid user exception when given user is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidUserException_WhenGivenUserIsNotValid()
        {
            this.user.Username = string.Empty;

            Assert.Throws<InvalidUserException>(() => this.userService.Insert(this.user));
        }

        /// <summary>
        /// Inserts the should throw invalid user exception when given user is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidUserExceptioneWithSpecificMessageException_WhenGivenUserIsNotValid()
        {
            this.user.Username = string.Empty;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidUserException' was thrown.";

            var exception = Assert.Throws<InvalidUserException>(() => this.userService.Insert(this.user));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Inserts the should throw invalid user exception when given password is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidUserException_WhenGivenPasswordIsNotValid()
        {
            this.user.Password = string.Empty;

            Assert.Throws<InvalidUserException>(() => this.userService.Insert(this.user));
        }

        /// <summary>
        /// Inserts the should throw invalid user exception when given password is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidUserExceptioneWithSpecificMessageException_WhenGivenPasswordIsNotValid()
        {
            this.user.Password = string.Empty;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidUserException' was thrown.";

            var exception = Assert.Throws<InvalidUserException>(() => this.userService.Insert(this.user));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Inserts the should throw invalid user exception when given email is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidUserException_WhenGivenEmailIsNotValid()
        {
            this.user.Email = string.Empty;

            Assert.Throws<InvalidUserException>(() => this.userService.Insert(this.user));
        }

        /// <summary>
        /// Inserts the should throw invalid user exception when given email is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidUserExceptioneWithSpecificMessageException_WhenGivenEmailIsNotValid()
        {
            this.user.Username = "email";
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidUserException' was thrown.";

            var exception = Assert.Throws<InvalidUserException>(() => this.userService.Insert(this.user));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should invoke user repository with given user.
        /// </summary>
        [Test]
        public void Update_ShouldInvokeUserRepository_WithGivenUser()
        {
            this.userService.Update(this.user);

            A.CallTo(() => this.userRepository.Update(this.user)).MustHaveHappened();
        }

        /// <summary>
        /// Updates the should throw invalid user exception when given user is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidUserException_WhenGivenUserIsNotValid()
        {
            this.user.Username = string.Empty;

            Assert.Throws<InvalidUserException>(() => this.userService.Update(this.user));
        }

        /// <summary>
        /// Updates the should throw invalid user exception when given user is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidUserWithSpecificMessageException_WhenGivenUserIsNotValid()
        {
            this.user.Username = string.Empty;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidUserException' was thrown.";

            var exception = Assert.Throws<InvalidUserException>(() => this.userService.Update(this.user));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should throw invalid user exception when given password is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidUserException_WhenGivenPasswordIsNotValid()
        {
            this.user.Password = string.Empty;

            Assert.Throws<InvalidUserException>(() => this.userService.Update(this.user));
        }

        /// <summary>
        /// Updates the should throw invalid user exception when given password is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidUserWithSpecificMessageException_WhenGivenPasswordIsNotValid()
        {
            this.user.Password = string.Empty;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidUserException' was thrown.";

            var exception = Assert.Throws<InvalidUserException>(() => this.userService.Update(this.user));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should throw invalid user exception when given email is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidUserException_WhenGivenEmailIsNotValid()
        {
            this.user.Email = string.Empty;

            Assert.Throws<InvalidUserException>(() => this.userService.Update(this.user));
        }

        /// <summary>
        /// Updates the should throw invalid user exception when given email is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidUserWithSpecificMessageException_WhenGivenEmailIsNotValid()
        {
            this.user.Email = "email";
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidUserException' was thrown.";

            var exception = Assert.Throws<InvalidUserException>(() => this.userService.Update(this.user));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }
    }
}
