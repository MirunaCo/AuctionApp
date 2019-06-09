// *********************************************************************************
// <copyright file="ScoreRepository.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using DomainModel;

    /// <summary>
    /// Class ScoreRepository.
    /// </summary>
    /// <seealso cref="DataMapper.IRepositoryScore" />
    public class ScoreRepository : IRepositoryScore
    {
        /// <summary>
        /// The database set.
        /// </summary>
        private readonly DbSet<Score> databaseSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreRepository" /> class.
        /// </summary>
        public ScoreRepository()
        {
            using (var context = new Model1Container())
            {
                this.databaseSet = context.Set<Score>();
            }
        }

        /// <summary>
        /// This method insert a score.
        /// </summary>
        /// <param name="score">The score.</param>
        public void Insert(Score score)
        {
            using (var context = new Model1Container())
            {
                context.Scores.Add(score);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List Score.</returns>
        public List<Score> Get(
            Expression<System.Func<Score, bool>> filter = null,
            System.Func<IQueryable<Score>, IOrderedQueryable<Score>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Score> query = this.databaseSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Score by id.</returns>
        public Score GetByID(int id)
        {
            using (var context = new Model1Container())
            {
                return context.Scores.Find(id);
            }
        }

        /// <summary>
        /// This method update a score.
        /// </summary>
        /// <param name="score">The score.</param>
        public void Update(Score score)
        {
            using (var context = new Model1Container())
            {
                context.Scores.Attach(score);
                context.Entry(score).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method delete a score.
        /// </summary>
        /// <param name="score">The score.</param>
        public void Delete(Score score)
        {
            using (var context = new Model1Container())
            {
                context.Scores.Remove(score);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(int id)
        {
            Score entityToDelete = this.databaseSet.Find(id);
            this.Delete(entityToDelete);
        }

        /// <summary>
        /// Gets all scores assigned to user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IEnumerable Score</returns>
        public IEnumerable<Score> GetAllScoresAssignedToUser(int userId)
        {
            return this.Get(s => s.User.Id.Equals(userId));
        }
    }
}
