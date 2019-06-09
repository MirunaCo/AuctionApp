// *********************************************************************************
// <copyright file="IRepositoryBid.cs" company="Transilvania University of Brasov">
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
    /// The IRepositoryBid.
    /// </summary>
    public interface IRepositoryBid
    {
        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="entityToDelete">The entityToDelete.</param>
        void Delete(Bid entityToDelete);

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        void Delete(int id);

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="bid">The bid parameter.</param>
        void Update(Bid bid);

        /// <summary>
        /// Get a bid.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        /// <returns>Returns a bid.</returns>
        Bid GetByID(int id);

        /// <summary>
        /// Gets by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List bids.</returns>
        List<Bid> Get(
            Expression<System.Func<Bid, bool>> filter = null,
            System.Func<IQueryable<Bid>, IOrderedQueryable<Bid>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Insert a bid.
        /// </summary>
        /// <param name="bid">The bid parameter.</param>
        void Insert(Bid bid);
    }
}