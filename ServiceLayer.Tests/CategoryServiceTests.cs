// ***********************************************************************
// <copyright file="CategoryServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Class categoryServiceTests
    /// </summary>
    [TestFixture]
    public class CategoryServiceTests
    {
        /// <summary>
        /// The category service
        /// </summary>
        private ICategoryServices categoryService;

        /// <summary>
        /// The category repository
        /// </summary>
        private IRepositoryCategory categoryRepository;

        /// <summary>
        /// The category
        /// </summary>
        private Category category;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.category = new Category { Id = new int(), Name = "ValidCategory", ParentCategory = new Category() };
            this.categoryRepository = A.Fake<IRepositoryCategory>();
            this.categoryService = new CategoryServices(this.categoryRepository);
        }

        /// <summary>
        /// Deletes the should invoke category repository with given identifier.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeCategoryRepository_WithGivenId()
        {
            this.categoryService.Delete(this.category.Id);

            A.CallTo(() => this.categoryRepository.Delete(this.category.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Deletes the should invoke category repository with given category.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeCategoryRepository_WithGivenCategory()
        {
            this.categoryService.Delete(this.category);

            A.CallTo(() => this.categoryRepository.Delete(this.category)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke category repository with given filter order and properties.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeCategoryRepository_WithGivenFilterOrderAndProperties()
        {
            Expression<Func<Category, bool>> expression = x => x.Name.Contains("computer");

            var currencies = this.categoryService.Get(expression, null, "Id");

            A.CallTo(() => this.categoryRepository.Get(expression, null, "Id")).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke category repository with given filter order and properties and return a not null value.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeCategoryRepository_WithGivenFilterOrderAndPropertiesAndReturnANotNullValue()
        {
            Expression<Func<Category, bool>> expression = x => x.Name.Contains("computer");

            var currencies = this.categoryService.Get(expression, null, "Id");

            Assert.IsNotNull(currencies);
        }

        /// <summary>
        /// Gets the by identifier should invoke category repository with given category identifier.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeCategoryRepository_WithGivenCategoryId()
        {
            var category = this.categoryService.GetByID(this.category.Id);

            A.CallTo(() => this.categoryRepository.GetByID(this.category.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the by identifier should invoke category repository with given category identifier and return a not null value.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeCategoryRepository_WithGivenCategoryIdAndReturnANotNullValue()
        {
            var category = this.categoryService.GetByID(this.category.Id);

            Assert.IsNotNull(category);
        }

        /// <summary>
        /// Inserts the should invoke category repository with given category.
        /// </summary>
        [Test]
        public void Insert_ShouldInvokeCategoryRepository_WithGivenCategory()
        {
            this.categoryService.Insert(this.category);

            A.CallTo(() => this.categoryRepository.Insert(this.category)).MustHaveHappened();
        }

        /// <summary>
        /// Inserts the should throw invalid category exception when given category is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidCategoryException_WhenGivenCategoryIsNotValid()
        {
            this.category.Name = string.Empty;

            Assert.Throws<InvalidCategoryException>(() => this.categoryService.Insert(this.category));
        }

        /// <summary>
        /// Inserts the should throw invalid category exception with specific message exception when given category is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidCategoryExceptionWithSpecificMessageException_WhenGivenCategoryIsNotValid()
        {
            this.category.Name = string.Empty;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidCategoryException' was thrown.";

            var exception = Assert.Throws<InvalidCategoryException>(() => this.categoryService.Insert(this.category));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Inserts the should throw invalid category exception when given category is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidCategoryException_WhenGivenCategoryNameIsNotValid()
        {
            this.category.Name = "nam";

            Assert.Throws<InvalidCategoryException>(() => this.categoryService.Insert(this.category));
        }

        /// <summary>
        /// Inserts the should throw invalid category exception with specific message exception when given category is not valid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidCategoryExceptionWithSpecificMessageException_WhenGivenCategoryNameIsNotValid()
        {
            this.category.Name = "nam";
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidCategoryException' was thrown.";

            var exception = Assert.Throws<InvalidCategoryException>(() => this.categoryService.Insert(this.category));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should invoke category repository with given category.
        /// </summary>
        [Test]
        public void Update_ShouldInvokeCategoryRepository_WithGivenCategory()
        {
            this.categoryService.Update(this.category);

            A.CallTo(() => this.categoryRepository.Update(this.category)).MustHaveHappened();
        }

        /// <summary>
        /// Updates the should throw invalid category exception when given category is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidCategoryException_WhenGivenCategoryIsNotValid()
        {
            this.category.Name = string.Empty;

            Assert.Throws<InvalidCategoryException>(() => this.categoryService.Update(this.category));
        }

        /// <summary>
        /// Updates the should throw invalid category exception with specific message exception when given category is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidCategoryExceptionWithSpecificMessageException_WhenGivenCategoryIsNotValid()
        {
            this.category.Name = string.Empty;
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidCategoryException' was thrown.";

            var exception = Assert.Throws<InvalidCategoryException>(() => this.categoryService.Update(this.category));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }

        /// <summary>
        /// Updates the should throw invalid category exception when given category name is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidCategoryException_WhenGivenCategoryNameIsNotValid()
        {
            this.category.Name = "name has more than fifty charactersname has more than fifty characters";

            Assert.Throws<InvalidCategoryException>(() => this.categoryService.Update(this.category));
        }

        /// <summary>
        /// Updates the should throw invalid category exception with specific message exception when given category name is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidCategoryExceptionWithSpecificMessageException_WhenGivenCategoryNameIsNotValid()
        {
            this.category.Name = "name has more than fifty charactersname has more than fifty characters";
            var exceptionMessage = "Exception of type 'ServiceLayer.Exception.InvalidCategoryException' was thrown.";

            var exception = Assert.Throws<InvalidCategoryException>(() => this.categoryService.Update(this.category));

            Assert.AreEqual(exception.Message, exceptionMessage);
        }
    }
}
