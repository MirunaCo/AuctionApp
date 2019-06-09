// *********************************************************************************
// <copyright file="IRepositoryScore.cs" company="Transilvania University of Brasov">
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
    /// The IRepositoryScore.
    /// </summary>
    public interface IRepositoryScore
    {
        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="entityToDelete">The entityToDelete.</param>
        void Delete(Score entityToDelete);

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        void Delete(int id);

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="score">The score parameter.</param>
        void Update(Score score);

        /// <summary>
        /// Get a client.
        /// </summary>
        /// <param name="id">The id parameter.</param>
        /// <returns>Returns a score.</returns>
        Score GetByID(int id);

        /// <summary>
        /// Gets by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List scores.</returns>
        List<Score> Get(
            Expression<System.Func<Score, bool>> filter = null,
            System.Func<IQueryable<Score>, IOrderedQueryable<Score>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Insert a score.
        /// </summary>
        /// <param name="score">The score parameter.</param>
        void Insert(Score score);

        /// <summary>
        /// Gets all scores assigned to user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IEnumerable Score</returns>
        IEnumerable<Score> GetAllScoresAssignedToUser(int userId);
    }
}