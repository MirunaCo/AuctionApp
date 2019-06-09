// *********************************************************************************
// <copyright file="ScoreServices.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataMapper;
    using DomainModel;
    using DomainModel.Validator;
    using Exception;
    using log4net;
    using ServiceInterface;

    /// <summary>
    /// Class ScoreServices.cs
    /// </summary>
    public class ScoreServices
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepositoryScore repository;

        /// <summary>
        /// The validator
        /// </summary>
        private readonly ScoreValidator validator;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserServices userService;

        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(ScoreServices));

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreServices" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="userService">The user service.</param>
        public ScoreServices(IRepositoryScore repository, IUserServices userService)
        {
            this.repository = repository;
            this.validator = new ScoreValidator();
            this.userService = userService;
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public void Delete(Score entityToDelete)
        {
            this.log.Info("delete a score");
            this.repository.Delete(entityToDelete);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.log.Info("delete a score");
            this.repository.Delete(id);
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable TEntity.</returns>
        public IEnumerable<Score> Get(Expression<Func<Score, bool>> filter = null, Func<IQueryable<Score>, IOrderedQueryable<Score>> orderBy = null, string includeProperties = "")
        {
            this.log.Info("get a list of scores");
            return this.repository.Get(filter, orderBy, includeProperties);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T Entity.</returns>
        public Score GetByID(int id)
        {
            this.log.Info("get a score");
            return this.repository.GetByID(id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ServiceLayer.Exception.InvalidScoreException">If is not a user product</exception>
        /// <exception cref="AuctionServiceLayer.Exception.InvalidScoreException">If is not a user product</exception>
        public void Insert(Score entity)
        {
            this.log.Info("insert a score");
            bool validationSucceeded = this.validator.Validate(entity).IsValid;

            if (validationSucceeded)
            {
                this.repository.Insert(entity);
                this.UpdateUserScore(entity.User);
            }
            else
            {
                throw new InvalidScoreException();
            }
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <exception cref="ServiceLayer.Exception.InvalidScoreException">If is not a user product</exception>
        /// <exception cref="AuctionServiceLayer.Exception.InvalidScoreException">If is not a user product</exception>
        public void Update(Score entityToUpdate)
        {
            this.log.Info("update a score");
            bool validationSucceeded = this.validator.Validate(entityToUpdate).IsValid;

            if (validationSucceeded)
            {
                this.repository.Update(entityToUpdate);
            }
            else
            {
                throw new InvalidScoreException();
            }
        }

        /// <summary>
        /// Updates the user score.
        /// </summary>
        /// <param name="user">The user.</param>
        private void UpdateUserScore(User user)
        {
            this.log.Info("update user score");
            this.SetUserScore(user);
            this.UpdateUser(user);
        }

        /// <summary>
        /// Sets the user score.
        /// </summary>
        /// <param name="user">The user.</param>
        private void SetUserScore(User user)
        {
            this.log.Info("set user score");
            var scoresOfUser = this.repository.GetAllScoresAssignedToUser(user.Id);
            var sumOfScores = scoresOfUser.Sum(s => s.Points);
            var countOfScore = scoresOfUser.Count();

            user.ScoreAverage = sumOfScores / countOfScore;
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        private void UpdateUser(User user)
        {
            this.log.Info("update user of a score");
            this.userService.Update(user);
        }
    }
}
