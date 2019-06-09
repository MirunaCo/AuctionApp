// *********************************************************************************
// <copyright file="IRepositoryUser.cs" company="Transilvania University of Brasov">
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
    /// The IRepositoryUser.
    /// </summary>
    public interface IRepositoryUser
    {
        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="entityToDelete">The entityToDelete.</param>
        void Delete(User entityToDelete);

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        void Delete(int id);

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="user">The user parameter.</param>
        void Update(User user);

        /// <summary>
        /// Get a client.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        /// <returns>Returns a client.</returns>
        User GetByID(int id);

        /// <summary>
        /// Gets by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List Users.</returns>
        List<User> Get(
            Expression<System.Func<User, bool>> filter = null,
            System.Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Insert a user.
        /// </summary>
        /// <param name="user">The client parameter.</param>
        void Insert(User user);

        /// <summary>
        /// Gets the user with products.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The User</returns>
        User GetUserWithProducts(int id);
    }
}
