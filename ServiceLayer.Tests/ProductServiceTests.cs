// ***********************************************************************
// <copyright file="ProductServiceTests.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// ***********************************************************************
namespace ServiceLayer.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using DataMapper;
    using DomainModel;
    using FakeItEasy;
    using NUnit.Framework;
    using ServiceLayer.Exception;
    using ServiceLayer.ServiceInterface;

    /// <summary>
    /// Class ProductServiceTests
    /// </summary>
    [TestFixture]
    public class ProductServiceTests
    {
        /// <summary>
        /// The product description
        /// </summary>
        private const string ProductDescription = "Product Description Product Description Product Description";

        /// <summary>
        /// The product name
        /// </summary>
        private const string ProductName = "Product Name";

        /// <summary>
        /// The product price
        /// </summary>
        private const double ProductPrice = 100.0d;

        /// <summary>
        /// The product service
        /// </summary>
        private IProductServices productService;

        /// <summary>
        /// The bid repository
        /// </summary>
        private IRepositoryProduct productRepository;

        /// <summary>
        /// The user repository
        /// </summary>
        private IRepositoryUser userRepository;

        /// <summary>
        /// The bid
        /// </summary>
        private Product product;

        /// <summary>
        /// The user
        /// </summary>
        private User user;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.InitializeProduct();

            this.productRepository = A.Fake<IRepositoryProduct>();
            this.userRepository = A.Fake<IRepositoryUser>();

            this.productService = new ProductServices(this.productRepository, this.userRepository);
        }

        /// <summary>
        /// Deletes the should invoke bid repository with given identifier.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeProductRepository_WithGivenId()
        {
            this.productService.Delete(this.product.Id);

            A.CallTo(() => this.productRepository.Delete(this.product.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Deletes the should invoke bid repository with given product.
        /// </summary>
        [Test]
        public void Delete_ShouldInvokeProductRepository_WithGivenProduct()
        {
            this.productService.Delete(this.product);

            A.CallTo(() => this.productRepository.Delete(this.product)).MustHaveHappened();
        }

        /// <summary>
        /// Finalizes the product should invoke update product repository with given product.
        /// </summary>
        [Test]
        public void FinalizeProduct_ShouldInvokeUpdateProductRepository_WithGivenProduct()
        {
            this.productService.FinalizeProduct(this.product.User, this.product);

            A.CallTo(() => this.productRepository.Update(this.product)).MustHaveHappened();
        }

        /// <summary>
        /// Finalizes the product should change product is active with given product.
        /// </summary>
        [Test]
        public void FinalizeProduct_ShouldChangeProductIsActive()
        {
            this.productService.FinalizeProduct(this.product.User, this.product);

            Assert.That(this.product.IsActive, Is.False);
        }

        /// <summary>
        /// Finalizes the product should throw unauthorized access exception when is not user product.
        /// </summary>
        [Test]
        public void FinalizeProduct_ShouldThrowUnauthorizedAccessException_WhenIsNotUserProduct()
        {
            this.product.User = new User() { Product = new List<Product>() };

            Assert.Throws<UnauthorizedAccessException>(() => this.productService.FinalizeProduct(this.product.User, this.product));
        }

        /// <summary>
        /// Gets the should invoke product repository with given filter order and properties.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeProductRepository_WithGivenFilterOrderAndProperties()
        {
            Expression<Func<Product, bool>> expression = x => x.User.Username.Contains("computer");

            var bid = this.productService.Get(expression, null, "Id");

            A.CallTo(() => this.productRepository.Get(expression, null, "Id")).MustHaveHappened();
        }

        /// <summary>
        /// Gets the should invoke product repository with given filter order and properties and return a not null value.
        /// </summary>
        [Test]
        public void Get_ShouldInvokeProductRepository_WithGivenFilterOrderAndPropertiesAndReturnANotNullValue()
        {
            Expression<Func<Product, bool>> expression = x => x.User.Username.Contains("computer");

            var bid = this.productService.Get(expression, null, "Id");

            Assert.IsNotNull(bid);
        }

        /// <summary>
        /// Gets the by identifier should invoke product repository with given bid identifier.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeProductRepository_WithGivenCategoryId()
        {
            var bid = this.productService.GetByID(this.product.Id);

            A.CallTo(() => this.productRepository.GetByID(this.product.Id)).MustHaveHappened();
        }

        /// <summary>
        /// Gets the by identifier should invoke product repository with given category identifier and return a not null value.
        /// </summary>
        [Test]
        public void GetByID_ShouldInvokeProductRepository_WithGivenCategoryIdAndReturnANotNullValue()
        {
            var bid = this.productService.GetByID(this.product.Id);

            Assert.IsNotNull(bid);
        }

        /// <summary>
        /// Inserts the should invoke product repository with given product.
        /// </summary>
        [Test]
        public void Insert_ShouldInvokeProductRepository_WithGivenProduct()
        {
            this.user.Product = null;
            this.userRepository = A.Fake<UserRepository>();
            A.CallTo(() => this.userRepository.GetUserWithProducts(this.user.Id)).Returns(this.user);

            this.productService.Insert(this.product);

            A.CallTo(() => this.productRepository.Insert(this.product)).MustHaveHappened();
        }

        /// <summary>
        /// Inserts the should invoke product repository with given product.
        /// </summary>
        [Test]
        public void Insert_ShouldInvokeProductRepository_WithGivenProductAndUserWithProducts()
        {
            this.productService.Insert(this.product);

            A.CallTo(() => this.productRepository.Insert(this.product)).MustHaveHappened();
        }

        /// <summary>
        /// Inserts the should throw duplicate product description when have duplicate description.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowDuplicateProductDescription_WhenHaveDuplicateDescription()
        {
            var otherProduct = new Product { Description = this.product.Description };

            A.CallTo(() => this.productRepository.GetAllExceptProductId(A<int>.Ignored)).Returns(new List<Product> { otherProduct });

            Assert.Throws<DuplicateProductDescription>(() => this.productService.Insert(this.product));
        }

        /// <summary>
        /// Inserts the should invoke product repository when have description.
        /// </summary>
        [Test]
        public void Insert_ShouldInvokeProductRepository_WhenHaveDescription()
        {
            var otherProduct = new Product { Id = 1, Name = "Product name22", Description = "Some descriptionSome descriptionSome descriptionSome descriptionSome description" };
            otherProduct.UserId = 1;
            A.CallTo(() => this.productRepository.GetAllExceptProductId(A<int>.Ignored)).Returns(new List<Product> { otherProduct });

            this.productService.Insert(this.product);

            A.CallTo(() => this.productRepository.Insert(this.product)).MustHaveHappened();
        }

        /// <summary>
        /// Inserts the should throw invalid product exception when have product name is invalid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidProductException_WhenHaveProductNameIsInvalid()
        {
            this.product.Name = string.Empty;

            Assert.Throws<InvalidProductException>(() => this.productService.Insert(this.product));
        }

        /// <summary>
        /// Inserts the should throw invalid product exception when have product is invalid.
        /// </summary>
        [Test]
        public void Insert_ShouldThrowInvalidProductException_WhenHaveProductIsInvalid()
        {
            this.product.Description = string.Empty;

            Assert.Throws<InvalidProductException>(() => this.productService.Insert(this.product));
        }

        /// <summary>
        /// Updates the should invoke score repository with given score.
        /// </summary>
        [Test]
        public void Update_ShouldInvokeProductRepository_WithGivenScore()
        {
            A.CallTo(() => this.productRepository.GetByID(this.product.Id)).Returns(this.product);

            this.productService.Update(this.product);

            A.CallTo(() => this.productRepository.Update(this.product)).MustHaveHappened();
        }

        /// <summary>
        /// Updates the should not call repository update when product is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldNotCallRepositoryUpdate_WhenProductIsNotValid()
        {
            this.product.Name = "Fail";
            A.CallTo(() => this.productRepository.GetByID(this.product.Id)).Returns(this.product);

            Assert.Throws<InvalidProductException>(() => this.productService.Update(this.product));
            A.CallTo(() => this.productRepository.Update(this.product)).MustNotHaveHappened();
        }

        /// <summary>
        /// Updates the should throw invalid product exception when product is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidProductException_WhenProductIsNotValid()
        {
            this.product.Name = string.Empty;
            A.CallTo(() => this.productRepository.GetByID(this.product.Id)).Returns(this.product);

            Assert.Throws<InvalidProductException>(() => this.productService.Update(this.product));
        }

        /// <summary>
        /// Updates the should not call repository update when product description is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldNotCallRepositoryUpdate_WhenProductDescriptionIsNotValid()
        {
            this.product.Description = "Fail";
            A.CallTo(() => this.productRepository.GetByID(this.product.Id)).Returns(this.product);

            Assert.Throws<InvalidProductException>(() => this.productService.Update(this.product));
            A.CallTo(() => this.productRepository.Update(this.product)).MustNotHaveHappened();
        }

        /// <summary>
        /// Updates the should throw invalid product exception when product description is not valid.
        /// </summary>
        [Test]
        public void Update_ShouldThrowInvalidProductException_WhenProductDescriptionIsNotValid()
        {
            this.product.Description = string.Empty;
            A.CallTo(() => this.productRepository.GetByID(this.product.Id)).Returns(this.product);

            Assert.Throws<InvalidProductException>(() => this.productService.Update(this.product));
        }

        /// <summary>
        /// Updates the status should change is active property.
        /// </summary>
        [Test]
        public void UpdateStatus_ShouldChangeIsActiveProperty()
        {
            A.CallTo(() => this.productRepository.GetAllActiveProducts()).Returns(new List<Product> { this.product });

            this.productService.UpdateProductsStatus();

            Assert.That(this.product.IsActive, Is.False);
        }

        /// <summary>
        /// Initializes the product.
        /// </summary>
        private void InitializeProduct()
        {
            this.product = new Product
            {
                Id = 1,
                IsActive = true,
                Name = ProductName,
                Price = ProductPrice,
                Bid = A.Fake<List<Bid>>(),
                Description = ProductDescription,
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now.AddSeconds(300)
            };

            this.user = new User
            {
                Id = 1,
                Username = "usernameUsr",
                Password = "usernamUsr12",
                Email = "usernamUsr@email.com",
                Product = new List<Product> { this.product },
                ScoreAverage = 5d
            };

            this.product.Currency = new Currency { Id = 1, Name = "ValidCurrency", Abbreviation = "VC" };
            this.product.Category = new Category { Id = 1, Name = "Category" };
            this.product.CurrencyId = 1;
            this.product.CategoryId = 1;
            this.product.User = this.user;
        }
    }
}
