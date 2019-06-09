// *********************************************************************************
// <copyright file="IRepositoryCategory.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace DataMapper
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DomainModel;

    /// <summary>
    /// The IRepositoryCategory.
    /// </summary>
    public interface IRepositoryCategory
    {
        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="entityToDelete">The entityToDelete.</param>
        void Delete(Category entityToDelete);

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        void Delete(int id);

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="category">The category parameter.</param>
        void Update(Category category);

        /// <summary>
        /// Get a client.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        /// <returns>Returns a category.</returns>
        Category GetByID(int id);

        /// <summary>
        /// Gets by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List categories.</returns>
        List<Category> Get(
            Expression<System.Func<Category, bool>> filter = null,
            System.Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Insert a score.
        /// </summary>
        /// <param name="category">The category parameter.</param>
        void Insert(Category category);
    }
}