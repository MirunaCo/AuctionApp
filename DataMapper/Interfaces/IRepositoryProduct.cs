// *********************************************************************************
// <copyright file="IRepositoryProduct.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DomainModel;

    /// <summary>
    /// The IRepositoryProduct.
    /// </summary>
    public interface IRepositoryProduct
    {
        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="entityToDelete">The entityToDelete.</param>
        void Delete(Product entityToDelete);

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        void Delete(int id);

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="product">The product parameter.</param>
        void Update(Product product);

        /// <summary>
        /// Get a client.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        /// <returns>Returns a product.</returns>
        Product GetByID(int id);

        /// <summary>
        /// Gets by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List products.</returns>
        List<Product> Get(
            Expression<System.Func<Product, bool>> filter = null,
            System.Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Insert a product.
        /// </summary>
        /// <param name="product">The product parameter.</param>
        void Insert(Product product);

        /// <summary>
        /// Gets all active products.
        /// </summary>
        /// <returns>List Product</returns>
        List<Product> GetAllActiveProducts();

        /// <summary>
        /// Gets all except product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>List Product</returns>
        List<Product> GetAllExceptProductId(int productId);
    }
}
