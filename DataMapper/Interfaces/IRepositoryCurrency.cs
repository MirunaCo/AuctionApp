// *********************************************************************************
// <copyright file="IRepositoryCurrency.cs" company="Transilvania University of Brasov">
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
    /// The IRepositoryCurrency.
    /// </summary>
    public interface IRepositoryCurrency
    {
        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="entityToDelete">The entityToDelete.</param>
        void Delete(Currency entityToDelete);

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        void Delete(int id);

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="currency">The currency parameter.</param>
        void Update(Currency currency);

        /// <summary>
        /// Get a currency.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        /// <returns>Returns a currency.</returns>
        Currency GetByID(int id);

        /// <summary>
        /// Gets by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List currencies.</returns>
        List<Currency> Get(
            Expression<System.Func<Currency, bool>> filter = null,
            System.Func<IQueryable<Currency>, IOrderedQueryable<Currency>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Insert a score.
        /// </summary>
        /// <param name="currency">The currency parameter.</param>
        void Insert(Currency currency);
    }
}