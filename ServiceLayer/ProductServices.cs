// *********************************************************************************
// <copyright file="ProductServices.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************

namespace ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;
    using DataMapper;
    using DomainModel;
    using DomainModel.Validator;
    using log4net;
    using ServiceInterface;
    using ServiceLayer.Exception;

    /// <summary>
    /// Class ProductServices
    /// </summary>
    /// <seealso cref="ServiceLayer.ServiceInterface.IProductServices" />
    public class ProductServices : IProductServices
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepositoryProduct repository;

        /// <summary>
        /// The validator
        /// </summary>
        private readonly ProductValidator validator;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IRepositoryUser userRepository;

        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(ProductServices));

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductServices" /> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public ProductServices(IRepositoryProduct productRepository, IRepositoryUser userRepository)
        {
            this.repository = productRepository;
            this.userRepository = userRepository;
            this.validator = new ProductValidator();
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public void Delete(Product entityToDelete)
        {
            this.log.Info("delete a product");
            this.repository.Delete(entityToDelete);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.log.Info("delete a product");
            this.repository.Delete(id);
        }

        /// <summary>
        /// Finalizes the product.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="product">The product.</param>
        /// <exception cref="UnauthorizedAccessException">Unauthorized Access</exception>
        /// <exception cref="System.UnauthorizedAccessException">If is not a user product</exception>
        public void FinalizeProduct(User user, Product product)
        {
            this.log.Info("finalize  product");
            if (product.IsActive)
            {
                var isUserProduct = user.Product.Any(p => p.Id.Equals(product.Id));
                if (isUserProduct)
                {
                    product.IsActive = false;
                    this.repository.Update(product);
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable TEntity.</returns>
        public IEnumerable<Product> Get(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "")
        {
            this.log.Info("get a list of products");
            return this.repository.Get(filter, orderBy, includeProperties);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T Entity.</returns>
        public Product GetByID(int id)
        {
            this.log.Info("get a product");
            return this.repository.GetByID(id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="LowUserRatingException">If entity is not valid</exception>
        /// <exception cref="InvalidProductException">If entity is not valid</exception>
        public void Insert(Product entity)
        {
            this.log.Info("insert a product");
            if (this.CanInsertProduct(entity.User))
            {
                this.InsertProduct(entity);
            }
            else
            {
                throw new LowUserRatingException();
            }
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <exception cref="ServiceLayer.Exception.InvalidProductException">If entity is not valid</exception>
        /// <exception cref="InvalidProductException">If entity is not valid</exception>
        public void Update(Product entityToUpdate)
        {
            this.log.Info("update a product");
            var isActive = this.IsProductActive(entityToUpdate.Id);
            if (isActive)
            {
                bool validationSucceeded = this.validator.Validate(entityToUpdate).IsValid;
                if (validationSucceeded)
                {
                    this.repository.Update(entityToUpdate);
                }
                else
                {
                    throw new InvalidProductException();
                }
            }
        }

        /// <summary>
        /// Updates the products status.
        /// </summary>
        public void UpdateProductsStatus()
        {
            this.log.Info("update a product status");
            var products = this.repository.GetAllActiveProducts();

            foreach (var product in products)
            {
                product.IsActive = false;
                this.repository.Update(product);
            }
        }

        /// <summary>
        /// Determines whether [is product valid] [the specified entity].
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if [is product valid] [the specified entity]; otherwise, <c>false</c>.</returns>
        /// <exception cref="InvalidProductException">If entity is not valid</exception>
        /// <exception cref="InvalidProductException">Product is not valid</exception>
        private bool IsProductValid(Product entity)
        {
            this.log.Info("validation of a product");
            var isProductValidForUser = this.IsProductValidForUser(entity.User.Id);

            var isProductValidForUserCategory = this.IsProductValidForUserCategories(entity);

            var validationResult = this.validator.Validate(entity);

            var validationSucceeded = validationResult.IsValid;

            var isProductDescriptionUnique = this.IsProductDescriptionUnique(entity);

            if (validationSucceeded)
            {
                return true;
            }

            throw new InvalidProductException();
        }

        /// <summary>
        /// Determines whether [is user score valid] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if [is user score valid] [the specified user]; otherwise, <c>false</c>.</returns>
        private bool CanInsertProduct(User user)
        {
            this.log.Info("validation insert of a product");
            var minScore = this.GetLowestRating();
            if (user.ScoreAverage < minScore)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether [is product description unique] [the specified entity].
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if [is product description unique] [the specified entity]; otherwise, <c>false</c>.</returns>
        /// <exception cref="DuplicateProductDescription">Product is not valid</exception>
        private bool IsProductDescriptionUnique(Product entity)
        {
            this.log.Info("validation description of a product");
            var description = entity.Description;
            var products = this.repository.GetAllExceptProductId(entity.Id);

            foreach (var product in products)
            {
                if (LevensteinDistance.Percentage(description, product.Description) > 75.0)
                {
                    throw new DuplicateProductDescription();
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether [is product active] [the specified product identifier].
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns><c>true</c> if [is product active] [the specified product identifier]; otherwise, <c>false</c>.</returns>
        private bool IsProductActive(int productId)
        {
            this.log.Info("validation actiove of a product");
            var originalProduct = this.repository.GetByID(productId);
            bool isActive = !(!originalProduct.IsActive || originalProduct.EndDate < DateTime.Now);

            return isActive;
        }

        /// <summary>
        /// Determines whether [is product valid for user categories] [the specified product].
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>If product is valid for User categories</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">If product is not valid</exception>
        private bool IsProductValidForUserCategories(Product product)
        {
            this.log.Info("validation user categories of a product");
            int maxProductPerCategory = this.GetMaxLimitOfProductsPerCategory();
            var user = this.userRepository.GetUserWithProducts(product.User.Id);

            if (user.Product == null)
            {
                return true;
            }

            return true;
        }

        /// <summary>
        /// Determines whether [is product valid for user] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if [is product valid for user] [the specified user identifier]; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If product is not valid</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If product is not valid</exception>
        private bool IsProductValidForUser(int userId)
        {
            this.log.Info("validation user of a product");
            int maxProducts = this.GetMaxLimitOfProducts();
            var user = this.userRepository.GetUserWithProducts(userId);

            if (user.Product.Count == 0)
            {
                return true;
            }

            var actualProductCount = user.Product.Count;

            var result = actualProductCount < maxProducts;

            if (result)
            {
                return true;
            }

            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Gets the maximum limit of products per category.
        /// </summary>
        /// <returns>Max limit of products per category.</returns>
        private int GetMaxLimitOfProductsPerCategory()
        {
            this.log.Info("limit max of a product");
            bool conversionResult = int.TryParse(ConfigurationManager.AppSettings["maxProductsPerCategory"], out int maxlimit);
            return maxlimit;
        }

        /// <summary>
        /// Gets the maximum limit of products.
        /// </summary>
        /// <returns>The maxim limit of products.</returns>
        private int GetMaxLimitOfProducts()
        {
            this.log.Info("max limit of a product");
            bool conversionResult = int.TryParse(ConfigurationManager.AppSettings["maxProducts"], out int maxlimit);
            return maxlimit;
        }

        /// <summary>
        /// Gets the lowest rating.
        /// </summary>
        /// <returns>Lowest rating</returns>
        private double GetLowestRating()
        {
            this.log.Info("lowest rating of a product");
            bool conversionResult = double.TryParse(ConfigurationManager.AppSettings["lowestRating"], out double lowestRating);
            return lowestRating;
        }

        /// <summary>
        /// Inserts the product.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">If product exceed maximum limit</exception>
        private void InsertProduct(Product entity)
        {
            this.log.Info("insert a product");
            if (this.IsProductValid(entity))
            {
                this.repository.Insert(entity);
            }
        }
    }
}